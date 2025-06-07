using System.Windows.Controls;
using SystemAnalysisAndDesign.ViewModels.AdminCarRentApprovalViewModel;

namespace SystemAnalysisAndDesign.Views.AdminCarRentApprovalView.ComponentAdminCarRentApprovalView;

public partial class AdminCarRentApprovalListView : UserControl
{
    public AdminCarRentApprovalListView()
    {
        InitializeComponent();
        DataContext = new AdminCarRentApprovalViewModel();
    }
}