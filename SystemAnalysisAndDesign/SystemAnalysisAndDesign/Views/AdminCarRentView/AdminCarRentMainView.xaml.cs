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

        private void AdminCarListView_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
