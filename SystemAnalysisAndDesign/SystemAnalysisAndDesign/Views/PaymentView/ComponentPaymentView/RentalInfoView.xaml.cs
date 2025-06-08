using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Interaction logic for ReantalInfoView.xaml
    /// </summary>
    public partial class RentalInfoView : UserControl
    {
        public RentalInfoView(RentalInfoViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        public string? SelectedPickUpLocation =>
    (cmbPickupLocation.SelectedItem as ComboBoxItem)?.Content?.ToString();

        public string? SelectedDropOffLocation =>
            (cmbDropoffLocation.SelectedItem as ComboBoxItem)?.Content?.ToString();

        public TimeSpan? SelectedPickUpTime
        {
            get
            {
                var content = (cmbPickupTime.SelectedItem as ComboBoxItem)?.Content?.ToString();
                return TimeSpan.TryParse(content, out var result) ? result : null;
            }
        }

        public TimeSpan? SelectedDropOffTime
        {
            get
            {
                var content = (cmbDropoffTime.SelectedItem as ComboBoxItem)?.Content?.ToString();
                return TimeSpan.TryParse(content, out var result) ? result : null;
            }
        }

    }
}
