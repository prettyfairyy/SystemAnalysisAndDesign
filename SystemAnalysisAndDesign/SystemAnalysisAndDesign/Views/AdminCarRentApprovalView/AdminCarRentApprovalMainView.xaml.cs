using System.Windows;
using SystemAnalysisAndDesign.ViewModels.AdminCarRentApprovalViewModel;
using SystemAnalysisAndDesign.Views.AdminCarRentApprovalView.ComponentAdminCarRentApprovalView;
using SystemAnalysisAndDesign.Views.AdminCarRentView;

namespace SystemAnalysisAndDesign.Views.AdminCarRentApprovalView;

public partial class AdminCarRentApprovalMainView : Window
{
    public AdminCarRentApprovalMainView()
    {
        InitializeComponent();

    }
    private void AdminCarRentApprovalListView_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement element)
        {
            element.DataContext = new AdminCarRentApprovalViewModel();
        }
    }

    private void btn_CarRentList(object sender, RoutedEventArgs e)
    {
        AdminCarRentMainView adminCarRentMainView = new AdminCarRentMainView();
        adminCarRentMainView.Show();
        this.Close();
    }
}