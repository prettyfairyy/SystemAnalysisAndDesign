using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SystemAnalysisAndDesign.Models;
using SystemAnalysisAndDesign.Models.Entities;
using SystemAnalysisAndDesign.ViewModels.Stores;

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

            using var context = new RentalDbContext();

            var account = context.UserAccount.FirstOrDefault(u =>
                u.Username == Username && u.PasswordHash == Password && u.IsActive);

            if (account == null)
            {
                MessageBox.Show("Invalid username or password.");
                return;
            }

            switch (account.Role?.ToLower())
            {
                case "customer":
                    var customer = context.Customers.FirstOrDefault(c => c.Username == Username);
                    if (customer != null)
                    {
                        LoginStore.CurrentCustomer = customer;
                        OnLoginSuccess?.Invoke();
                    }
                    else
                    {
                        MessageBox.Show("Customer record not found.");
                    }
                    break;

                case "employee":
                    var employee = context.Employee.FirstOrDefault(e => e.Username == Username);
                    if (employee != null)
                    {
                        LoginStore.CurrentEmployee = employee;
                        OnAdminLoginSuccess?.Invoke();
                    }
                    else
                    {
                        MessageBox.Show("Admin employee record not found.");
                    }
                    break;

                default:
                    MessageBox.Show("Unsupported user role.");
                    break;
            }
        }

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
