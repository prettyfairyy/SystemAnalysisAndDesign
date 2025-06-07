using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SystemAnalysisAndDesign.Models;
using SystemAnalysisAndDesign.ViewModels.AdminCarRentViewModel;
using SystemAnalysisAndDesign.ViewModels.SignInandRegisterViewModel;
using SystemAnalysisAndDesign.Views.AdminCarRentApprovalView;
using SystemAnalysisAndDesign.Views.PaymentView;

namespace SystemAnalysisAndDesign.Views.AdminCarRentView
{
    /// <summary>
    /// Interaction logic for AdminCarRentMainView.xaml
    /// </summary>
    public partial class AdminCarRentMainView : Window
    {
        private readonly RentalDbContext _context;
        public AdminCarRentMainView()
        {
            InitializeComponent();

            _context = new RentalDbContext();
            this.DataContext = new AdminCarListViewModel(_context);
        }

        private void btn_AddCar(object sender, RoutedEventArgs e)
        {
            AdminCarRentAddMainView adminCarRentAddMainView = new AdminCarRentAddMainView();
            adminCarRentAddMainView.Show();
            this.Close();
        }
        private void btn_CarRentList(object sender, RoutedEventArgs e)
        {
            AdminCarRentMainView adminCarRentMainView = new AdminCarRentMainView();
            adminCarRentMainView.Show();
            this.Close();
        }
        private void btn_CarRental(object sender, RoutedEventArgs e)
        {
            AdminCarRentApprovalMainView adminCarRentApprovalMainView = new AdminCarRentApprovalMainView();
            adminCarRentApprovalMainView.Show();
            this.Close();
        }
    }
}
