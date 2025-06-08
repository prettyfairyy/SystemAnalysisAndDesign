using System.Windows;
using System.Windows.Input;
using SystemAnalysisAndDesign.ViewModels.SignInandRegisterViewModel;

namespace SystemAnalysisAndDesign.Views.SignInandRegisterView
{
    public partial class SignInMainView : Window
    {
        private bool isPasswordVisible = false;

        public SignInMainView()
        {
            InitializeComponent();

            var vm = new SignInViewModel();
            vm.OnLoginSuccess = ShowCategoryCarRentMainView;
            vm.OnAdminLoginSuccess = ShowAdminDashboardMainView;
            this.DataContext = vm;
        }

        private void ShowCategoryCarRentMainView()
        {
            var categoryCarRentMainView = new CategoryCarRentView.CategoryCarRentMainView();
            categoryCarRentMainView.Show();
            this.Close();
        }

        private void ShowAdminDashboardMainView()
        {
            var adminView = new AdminDashboardView.AdminDashboardMainView();
            adminView.Show();
            this.Close();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && DataContext is SignInViewModel vm)
            {
                vm.Password = txtPassword.Password;
                vm.LoginCommand.Execute(null);
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void btnShowPassword_Click(object sender, RoutedEventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;

            if (isPasswordVisible)
            {
                txtVisiblePassword.Text = txtPassword.Password;
                txtVisiblePassword.Visibility = Visibility.Visible;
                txtPassword.Visibility = Visibility.Collapsed;
                btnShowPassword.Content = "🙈";
            }
            else
            {
                txtPassword.Password = txtVisiblePassword.Text;
                txtPassword.Visibility = Visibility.Visible;
                txtVisiblePassword.Visibility = Visibility.Collapsed;
                btnShowPassword.Content = "👁";
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is SignInViewModel vm)
            {
                vm.Username = txtUsername.Text;
                vm.Password = isPasswordVisible ? txtVisiblePassword.Text : txtPassword.Password;
                vm.LoginCommand.Execute(null);
            }
        }
    }
}
