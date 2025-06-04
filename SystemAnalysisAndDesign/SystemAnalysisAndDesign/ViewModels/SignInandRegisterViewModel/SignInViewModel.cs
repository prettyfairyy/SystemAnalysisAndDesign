using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SystemAnalysisAndDesign.Models;
using SystemAnalysisAndDesign.ViewModels.Stores;
using static SystemAnalysisAndDesign.Views.DetailCarRentView.ComponentDetailCarRentView.DetailCarView;

namespace SystemAnalysisAndDesign.ViewModels.SignInandRegisterViewModel
{
    public class SignInViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; }
        public Action? OnLoginSuccess { get; set; }
        public Action? OnAdminLoginSuccess { get; set; }

        public SignInViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            // Kiểm tra nếu là admin
            if (Username == "admin" && Password == "123456")
            {
                OnAdminLoginSuccess?.Invoke();
                return;
            }

            using var context = new RentalDbContext();
            var customer = context.Customers.FirstOrDefault(
                c => c.Username == Username && c.PasswordHash == Password);

            if (customer != null)
            {
                LoginStore.CurrentCustomer = customer;
                OnLoginSuccess?.Invoke();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
