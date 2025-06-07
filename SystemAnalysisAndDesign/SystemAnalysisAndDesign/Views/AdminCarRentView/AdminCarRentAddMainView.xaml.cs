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

namespace SystemAnalysisAndDesign.Views.AdminCarRentView
{
    /// <summary>
    /// Interaction logic for AdminCarRentAddMainView.xaml
    /// </summary>
    public partial class AdminCarRentAddMainView : Window
    {
        public AdminCarRentAddMainView()
        {
            InitializeComponent();
        }

        private void AdminAddCarView_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void btn_CarRent(object sender, RoutedEventArgs e)
        {
            AdminCarRentMainView adminCarRentMainView = new AdminCarRentMainView();
            adminCarRentMainView.Show();
            this.Close();
        }
        
    }
}
