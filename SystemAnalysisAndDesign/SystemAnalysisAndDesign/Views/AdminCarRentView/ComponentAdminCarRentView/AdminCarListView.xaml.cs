using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SystemAnalysisAndDesign.Models;
using SystemAnalysisAndDesign.Models.Entities;
using SystemAnalysisAndDesign.ViewModels.AdminCarRentViewModel;

namespace SystemAnalysisAndDesign.Views.AdminCarRentView.ComponentAdminCarRentView
{
    /// <summary>
    /// Interaction logic for AdminCarList.xaml
    /// </summary>
    public partial class AdminCarListView : UserControl
    {
        public AdminCarListView()
        {
            InitializeComponent();
            var context = new RentalDbContext();
            var viewModel = new AdminCarListViewModel(context);
            this.DataContext = viewModel;
        }
        /*private void OnEditClicked(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = CarRentDataGrid.SelectedItem as CarModel; // Thay CarModel bằng model của bạn
            if (selectedItem != null)
            {
                // Mở dialog chỉnh sửa...
            }
        }

        private void OnDeleteClicked(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = CarRentDataGrid.SelectedItem as CarModel;
            if (selectedItem != null)
            {
                // Thực hiện xóa...
            }
        }*/
    }
}