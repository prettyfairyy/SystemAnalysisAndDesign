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
using SystemAnalysisAndDesign.ViewModels.CategoryCarRentViewModel;

namespace SystemAnalysisAndDesign.Views.CategoryCarRentView
{
    /// <summary>
    /// Interaction logic for CategoryCarRentMainView.xaml
    /// </summary>
    public partial class CategoryCarRentMainView : Window
    {
        private readonly RentalDbContext _context;

        public CategoryCarRentMainView()
        {
            InitializeComponent();

            _context = new RentalDbContext();
            var viewModel = new FilterOptionViewModel(_context);
            this.DataContext = viewModel;
            
            viewModel.CloseAction = new Action(() => this.Close());
        }
    }
}
