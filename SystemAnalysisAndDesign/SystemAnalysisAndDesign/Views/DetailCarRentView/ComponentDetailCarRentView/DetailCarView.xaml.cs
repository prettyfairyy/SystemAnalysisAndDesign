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
using SystemAnalysisAndDesign.Models.Entities;
using SystemAnalysisAndDesign.ViewModels.DetailCarRentViewModel;
using SystemAnalysisAndDesign.ViewModels.PaymentViewModel;
using SystemAnalysisAndDesign.Views.PaymentView;

namespace SystemAnalysisAndDesign.Views.DetailCarRentView.ComponentDetailCarRentView
{
    /// <summary>
    /// Interaction logic for DetailCarView.xaml
    /// </summary>
    public partial class DetailCarView : UserControl
    {
        public DetailCarView()
        {
            InitializeComponent();
        }
        private void RentNowButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Car selectedCar)
            {
                // Gán vào store dùng chung
                SelectedCarStore.SelectedCar = selectedCar;

                // Điều hướng sang màn hình thanh toán
                PaymentMainView paymentWindow = new PaymentMainView();
                paymentWindow.Show();
                Window.GetWindow(this)?.Close();
            }
        }

    }
}
