
using System.Windows.Controls;
using SystemAnalysisAndDesign.Models;
using SystemAnalysisAndDesign.ViewModels.AdminCarRentViewModel;
using SystemAnalysisAndDesign.Views;

namespace SystemAnalysisAndDesign.Views.AdminCarRentView.ComponentAdminCarRentView
{
    /// <summary>
    /// Interaction logic for AdminAddCarView.xaml
    /// </summary>
    public partial class AdminAddCarView : UserControl
    {
        public AdminAddCarView()
        {
            InitializeComponent();
            var context = new RentalDbContext();
            var carListViewModel = new AdminCarListViewModel(context); // hoặc truyền đối tượng phù hợp
            this.DataContext = new AdminAddCarViewModel(context, carListViewModel);
        }
    }
}
