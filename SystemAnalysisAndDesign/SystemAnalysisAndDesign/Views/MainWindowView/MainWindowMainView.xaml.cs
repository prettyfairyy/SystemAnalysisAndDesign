using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SystemAnalysisAndDesign.Models;
using SystemAnalysisAndDesign.ViewModels.MainWindowViewModel;
using SystemAnalysisAndDesign.ViewModels.SignInandRegisterViewModel;
using SystemAnalysisAndDesign.Views.CategoryCarRentView;
using SystemAnalysisAndDesign.Views.PaymentView;
using SystemAnalysisAndDesign.Views.SignInandRegisterView;

namespace SystemAnalysisAndDesign.Views.MainWindowView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowMainView : Window
    {
        public MainWindowMainView()
        {
            InitializeComponent();
            var context = new RentalDbContext(); // sẽ trigger EnsureCreated
            this.DataContext = new MainWindowMainViewModel();
        }
        private void btn_RentCarMainWindow(object sender, RoutedEventArgs e)
        {
            SignInMainView signInMainView = new SignInMainView();
            signInMainView.Show();
            this.Close();
        }
    }
}