using System;
using System.Collections.Generic;
using System.Linq;

using System.Windows;
using System.Windows.Controls;
using SystemAnalysisAndDesign.Models.Entities;
using SystemAnalysisAndDesign.ViewModels.Stores;
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
                SelectedCarStore.SelectedCar = selectedCar;

                //Truyền Customer đang đăng nhập
                var loggedInCustomer = LoginStore.CurrentCustomer;  // bạn phải đảm bảo không null
                if (loggedInCustomer == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng. Vui lòng đăng nhập lại.");
                    return;
                }

                var paymentWindow = new PaymentMainView();
                paymentWindow.Show();
                Window.GetWindow(this)?.Close();
            }
        }


    }
}
