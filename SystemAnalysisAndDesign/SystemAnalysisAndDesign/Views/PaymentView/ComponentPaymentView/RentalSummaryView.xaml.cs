using System.Windows;
using System.Windows.Controls;
using SystemAnalysisAndDesign.ViewModels.PaymentViewModel;

namespace SystemAnalysisAndDesign.Views.PaymentView.ComponentPaymentView
{
    /// <summary>
    /// Interaction logic for RentalSummaryView.xaml
    /// </summary>
    public partial class RentalSummaryView : UserControl
    {
        public RentalSummaryView(RentalInfoViewModel rentalInfoViewModel)
        {
            InitializeComponent();
            DataContext = new RentalSummaryViewModel(rentalInfoViewModel);
        }
        
    }
}
