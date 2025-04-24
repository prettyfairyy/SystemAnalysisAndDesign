using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CarRentalSystem.Helpers;
using CarRentalSystem.Models.Entities;
using CarRentalSystem.Models.Services;

namespace CarRentalSystem.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly AuthenticationService _authService;
        private string _username;
        private string _password;
        private bool _rememberMe;
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

        public bool RememberMe
        {
            get => _rememberMe;
            set => SetProperty(ref _rememberMe, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand ForgotPasswordCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel()
        {
            _authService = new AuthenticationService();
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
            ForgotPasswordCommand = new RelayCommand(ExecuteForgotPassword);
            RegisterCommand = new RelayCommand(ExecuteRegister);
        }

        private bool CanExecuteLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteLogin(object parameter)
        {
            try
            {
                User user = _authService.AuthenticateUser(Username, Password);

                if (user != null)
                {
                    // Lưu trạng thái "Remember me"
                    _authService.SaveRememberMePreference(user.UserId, RememberMe);

                    // Chuyển đến màn hình chính
                    // Trong thực tế, bạn sẽ sử dụng một service để điều hướng
                    MessageBox.Show($"Đăng nhập thành công! Xin chào {user.Username}");

                    // TODO: Chuyển đến màn hình chính
                    // NavigationService.Navigate(new Uri("/Views/MainWindow.xaml", UriKind.Relative));
                }
                else
                {
                    ErrorMessage = "Tên đăng nhập hoặc mật khẩu không chính xác!";
                }
            }
            catch (System.Exception ex)
            {
                ErrorMessage = $"Lỗi đăng nhập: {ex.Message}";
            }
        }

        private void ExecuteForgotPassword(object parameter)
        {
            // TODO: Xử lý quên mật khẩu
            MessageBox.Show("Chức năng quên mật khẩu sẽ được triển khai sau.");
        }

        private void ExecuteRegister(object parameter)
        {
            // TODO: Chuyển đến màn hình đăng ký
            // NavigationService.Navigate(new Uri("/Views/RegisterView.xaml", UriKind.Relative));
            MessageBox.Show("Chuyển đến màn hình đăng ký");
        }
    }
}

