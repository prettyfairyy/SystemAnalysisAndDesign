using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CarRentalSystem.Helpers;
using CarRentalSystem.Models.Services;

namespace CarRentalSystem.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly AuthenticationService _authService;
        private string _username;
        private string _password;
        private string _confirmPassword;
        private string _email;
        private string _errorMessage;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand RegisterCommand { get; }
        public ICommand BackToLoginCommand { get; }

        public RegisterViewModel()
        {
            _authService = new AuthenticationService();
            RegisterCommand = new RelayCommand(ExecuteRegister, CanExecuteRegister);
            BackToLoginCommand = new RelayCommand(ExecuteBackToLogin);
        }

        private bool CanExecuteRegister(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Username) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   !string.IsNullOrWhiteSpace(ConfirmPassword) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   Password == ConfirmPassword;
        }

        private void ExecuteRegister(object parameter)
        {
            try
            {
                if (Password != ConfirmPassword)
                {
                    ErrorMessage = "Mật khẩu xác nhận không khớp!";
                    return;
                }

                bool result = _authService.RegisterUser(Username, Password, Email);

                if (result)
                {
                    MessageBox.Show("Đăng ký tài khoản thành công!");

                    // Chuyển về màn hình đăng nhập
                    ExecuteBackToLogin(null);
                }
                else
                {
                    ErrorMessage = "Tên đăng nhập đã tồn tại!";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Lỗi đăng ký: {ex.Message}";
            }
        }

        private void ExecuteBackToLogin(object parameter)
        {
            // TODO: Quay lại màn hình đăng nhập
            // NavigationService.Navigate(new Uri("/Views/LoginView.xaml", UriKind.Relative));
            MessageBox.Show("Quay lại màn hình đăng nhập");
        }
    }
}

