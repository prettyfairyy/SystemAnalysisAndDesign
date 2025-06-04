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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SystemAnalysisAndDesign.ViewModels.PaymentViewModel;

namespace SystemAnalysisAndDesign.Views.PaymentView.ComponentPaymentView
{
    /// <summary>
    /// Interaction logic for RentalInfoView.xaml
    /// </summary>
    public partial class BillingInfoView : UserControl
    {
        public BillingInfoView()
        {
            InitializeComponent();
            DataContext = new BillingInfoViewModel();
        }

    }
}
