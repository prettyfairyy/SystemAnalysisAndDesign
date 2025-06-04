using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SystemAnalysisAndDesign.Models;
using SystemAnalysisAndDesign.Models.Entities;
using SystemAnalysisAndDesign.ViewModels.DetailCarRentViewModel;
using SystemAnalysisAndDesign.ViewModels.PaymentViewModel;
using SystemAnalysisAndDesign.ViewModels.Stores;
using SystemAnalysisAndDesign.Views.PaymentView.ComponentPaymentView;

namespace SystemAnalysisAndDesign.Views.PaymentView
{
    /// <summary>
    /// Interaction logic for PaymentMainView.xaml
    /// </summary>
    public partial class PaymentMainView : Window
    {
        private readonly RentalInfoViewModel rentalInfoVM = new RentalInfoViewModel();

        public PaymentMainView()
        {
            InitializeComponent();

            RentalInfoViewControl.Content = new RentalInfoView(rentalInfoVM);
            RentalSummaryPlaceholder.Content = new RentalSummaryView(rentalInfoVM);
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Quay lại trang MainWindowMainView
            var categoryView = new CategoryCarRentView.CategoryCarRentMainView();
            categoryView.Show();
            this.Close();
        }


        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Tag.ToString();
                textBox.Foreground = Brushes.Gray;
            }
        }
               
        private static bool IsDigitAllowed(string text)
        {
            return Regex.IsMatch(text, @"^\d+$");
        }

        private bool ValidateForm()
        {                    
            if (chkTerms.IsChecked != true)
            {
                MessageBox.Show("Vui lòng đồng ý với điều khoản và điều kiện để tiếp tục.", "Điều khoản và điều kiện", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
        private void btnRentNow_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateForm()) return;

            using (var context = new RentalDbContext())
            {
                // === 1. Tạo RentalId ===
                int lastRentalCount = context.Rentals.Count();  // hoặc dùng Max(RentalId) nếu bạn có định dạng phức tạp
                string newRentalId = Rental.GenerateNextId(lastRentalCount); // Bạn cần tạo hàm này trong class Rental

                // === 2. Tạo Rental entity ===
                var selectedCar = SelectedCarStore.SelectedCar;
                var customer = LoginStore.CurrentCustomer;
                var rentalVM = rentalInfoVM;

                var rentalInfoControl = (RentalInfoView)RentalInfoViewControl.Content;

                rentalInfoVM.PickUpLocation = rentalInfoControl.SelectedPickUpLocation ?? string.Empty; ;
                rentalInfoVM.DropOffLocation = rentalInfoControl.SelectedDropOffLocation ?? string.Empty;
                rentalInfoVM.PickUpTime = rentalInfoControl.SelectedPickUpTime;
                rentalInfoVM.DropOffTime = rentalInfoControl.SelectedDropOffTime;
                string selectedMethod = "Cash";
                if (rbCreditCard.IsChecked == true) selectedMethod = "Credit Card";
                else if (rbPayPal.IsChecked == true) selectedMethod = "PayPal";
                else if (rbBitcoin.IsChecked == true) selectedMethod = "Bitcoin";


                var rental = new Rental
                {

                    RentalId = newRentalId,
                    CustomerId = customer.CustomerId,
                    CarId = selectedCar.CarId,
                    PickUpLocation = rentalVM.PickUpLocation,
                    PickUpDate = rentalVM.PickUpDate,
                    PickUpTime = rentalVM.PickUpTime,
                    DropOffLocation = rentalVM.DropOffLocation,
                    DropOffDate = rentalVM.DropOffDate,
                    DropOffTime = rentalVM.DropOffTime,
                    RentalStatus = "waiting",
                    TotalAmount = CalculateTotalAmount(selectedCar, rentalVM)
                };

                // === 3. Tạo PaymentId ===
                int lastPaymentCount = context.Payments.Count();
                string newPaymentId = Payment.GenerateNextId(lastPaymentCount);

                var payment = new Payment
                {
                    PaymentMethod = selectedMethod,
                    PaymentId = newPaymentId,
                    RentalId = newRentalId,
                    PaymentDate = DateTime.Now,
                    PaymentAmount = rental.TotalAmount,
                    IsPaid = false
                };

                context.Rentals.Add(rental);
                context.Payments.Add(payment);
                context.SaveChanges();
            }

            MessageBox.Show("Xe của bạn đã được đặt.","Hãy chờ xác nhận", MessageBoxButton.OK, MessageBoxImage.Information);

            var categoryView = new CategoryCarRentView.CategoryCarRentMainView();
            categoryView.Show();
            this.Close();
        }
        private decimal CalculateTotalAmount(Car car, RentalInfoViewModel vm)
        {
            if (car == null || vm.PickUpDate == null || vm.DropOffDate == null)
                return 0;

            int days = (vm.DropOffDate.Value - vm.PickUpDate.Value).Days;
            return days <= 0 ? 0 : days * (car.PricePerDay ?? 0);
        }
    }
}
