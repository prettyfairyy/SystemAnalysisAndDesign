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
using SystemAnalysisAndDesign.Models.Entities;
using SystemAnalysisAndDesign.ViewModels.CategoryCarRentViewModel;
using SystemAnalysisAndDesign.ViewModels.DetailCarRentViewModel;
using SystemAnalysisAndDesign.ViewModels.PaymentViewModel;
using SystemAnalysisAndDesign.Views.CategoryCarRentView.ComponentCategoryCarRentView;
using SystemAnalysisAndDesign.Views.PaymentView;

namespace SystemAnalysisAndDesign.Views.DetailCarRentView
{
    /// <summary>
    /// Interaction logic for DetailCarRentMainView.xaml
    /// </summary>
    public partial class DetailCarRentMainView : Window
    {
        /*public DetailCarRentMainView(Car detailCar, FilterOptionViewModel filterVM)
        {
            InitializeComponent();
            // Gán filterVM vào resource của view
            Resources["FilterOptionVM"] = filterVM;
            DataContext = new DetailCarViewModel(detailCar);
        }*/

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Quay lại trang CategoryCarRentMainView
            var categoryView = new CategoryCarRentView.CategoryCarRentMainView();
            categoryView.Show();
            this.Close();
        }

        //public DetailCarRentMainView() : this(new Car()) { }
        public DetailCarRentMainView(Car detailCar, FilterOptionViewModel filterVM)
        {
            InitializeComponent();

            FilterOptionView.DataContext = filterVM;
            FilterOptionView.IsEnabled = false;

            DataContext = new DetailCarViewModel(detailCar);
        }

    }
}
