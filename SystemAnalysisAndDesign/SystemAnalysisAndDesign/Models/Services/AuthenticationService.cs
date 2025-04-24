using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using CarRentalSystem.Models.Entities;
using CarRentalSystem.Helpers;

namespace CarRentalSystem.Models.Services
{
    public class AuthenticationService
    {
        private readonly DatabaseService _databaseService;

        public AuthenticationService()
        {
            _databaseService = new DatabaseService();
        }

        public User AuthenticateUser(string username, string password)
        {
            string query = "SELECT * FROM Users WHERE Username = @Username";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", username)
            };

            DataTable result = _databaseService.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0)
                return null;

            DataRow userRow = result.Rows[0];
            string storedHash = userRow["PasswordHash"].ToString();
            string storedSalt = userRow["Salt"].ToString();

            // Kiểm tra mật khẩu
            if (PasswordHelper.VerifyPassword(password, storedHash, storedSalt))
            {
                // Cập nhật thời gian đăng nhập cuối
                UpdateLastLogin(userRow["UserId"].ToString());

                // Trả về thông tin người dùng
                return new User
                {
                    UserId = userRow["UserId"].ToString(),
                    Username = userRow["Username"].ToString(),
                    Email = userRow["Email"].ToString(),
                    IsAdmin = Convert.ToBoolean(userRow["IsAdmin"]),
                    CreatedDate = Convert.ToDateTime(userRow["CreatedDate"]),
                    LastLogin = userRow["LastLogin"] != DBNull.Value ? Convert.ToDateTime(userRow["LastLogin"]) : (DateTime?)null
                };
            }

            return null;
        }

        public bool RegisterUser(string username, string password, string email, bool isAdmin = false)
        {
            // Kiểm tra username đã tồn tại chưa
            if (IsUsernameExists(username))
                return false;

            // Tạo salt và hash mật khẩu
            string salt = PasswordHelper.GenerateSalt();
            string passwordHash = PasswordHelper.HashPassword(password, salt);

            // Tạo ID mới
            string userId = "U" + Guid.NewGuid().ToString("N").Substring(0, 8);

            string query = @"
                INSERT INTO Users (UserId, Username, PasswordHash, Salt, Email, IsAdmin, CreatedDate)
                VALUES (@UserId, @Username, @PasswordHash, @Salt, @Email, @IsAdmin, @CreatedDate)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@Username", username),
                new SqlParameter("@PasswordHash", passwordHash),
                new SqlParameter("@Salt", salt),
                new SqlParameter("@Email", email),
                new SqlParameter("@IsAdmin", isAdmin),
                new SqlParameter("@CreatedDate", DateTime.Now)
            };

            int rowsAffected = _databaseService.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        private bool IsUsernameExists(string username)
        {
            string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", username)
            };

            object result = _databaseService.ExecuteScalar(query, parameters);
            return Convert.ToInt32(result) > 0;
        }

        private void UpdateLastLogin(string userId)
        {
            string query = "UPDATE Users SET LastLogin = @LastLogin WHERE UserId = @UserId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LastLogin", DateTime.Now),
                new SqlParameter("@UserId", userId)
            };

            _databaseService.ExecuteNonQuery(query, parameters);
        }

        public bool SaveRememberMePreference(string userId, bool rememberMe)
        {
            string query = "UPDATE Users SET RememberMe = @RememberMe WHERE UserId = @UserId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RememberMe", rememberMe),
                new SqlParameter("@UserId", userId)
            };

            int rowsAffected = _databaseService.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }
    }
}
