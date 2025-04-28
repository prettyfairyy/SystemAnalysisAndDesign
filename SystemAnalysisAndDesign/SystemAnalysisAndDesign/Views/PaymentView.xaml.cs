using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CarRentalSystem
{
    public partial class PaymentView : Window
    {
        public PaymentView()
        {
            InitializeComponent();
            SetupEventHandlers();

            // Đặt giá trị mặc định cho DatePicker
            dpPickupDate.SelectedDate = DateTime.Today.AddDays(1);
            dpDropoffDate.SelectedDate = DateTime.Today.AddDays(3);
        }

        private void SetupEventHandlers()
        {
            // Xử lý định dạng số thẻ tín dụng
            txtCardNumber.TextChanged += (s, e) => {
                FormatCreditCardNumber();
            };

            // Xử lý định dạng ngày hết hạn
            txtExpirationDate.TextChanged += (s, e) => {
                FormatExpiryDate();
            };

            // Xử lý định dạng số điện thoại
            txtPhoneNumber.PreviewTextInput += (s, e) => {
                e.Handled = !IsDigitAllowed(e.Text);
            };

            // Chỉ cho phép nhập số trong CVC
            txtCVC.PreviewTextInput += (s, e) => {
                e.Handled = !IsDigitAllowed(e.Text);
            };
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = string.Empty;
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Tag.ToString();
                textBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void FormatCreditCardNumber()
        {
            string text = Regex.Replace(txtCardNumber.Text, @"\D", "");

            if (text.Length > 0)
            {
                // Format: XXXX XXXX XXXX XXXX
                text = Regex.Replace(text, @"(\d{4})(\d{0,4})(\d{0,4})(\d{0,4})", m => {
                    string result = m.Groups[1].Value;
                    if (m.Groups[2].Length > 0) result += " " + m.Groups[2].Value;
                    if (m.Groups[3].Length > 0) result += " " + m.Groups[3].Value;
                    if (m.Groups[4].Length > 0) result += " " + m.Groups[4].Value;
                    return result;
                });
            }

            txtCardNumber.Text = text;
            txtCardNumber.SelectionStart = txtCardNumber.Text.Length;
        }

        private void FormatExpiryDate()
        {
            string text = Regex.Replace(txtExpirationDate.Text, @"\D", "");

            if (text.Length > 0)
            {
                // Format: MM/YY
                if (text.Length > 2)
                {
                    text = text.Substring(0, 2) + "/" + text.Substring(2, Math.Min(2, text.Length - 2));
                }
            }

            txtExpirationDate.Text = text;
            txtExpirationDate.SelectionStart = txtExpirationDate.Text.Length;
        }

        private static bool IsDigitAllowed(string text)
        {
            return Regex.IsMatch(text, @"^\d+$");
        }

        private void btnRentNow_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra thông tin thanh toán
            if (!ValidateForm())
            {
                return;
            }

            // Xử lý thanh toán và thuê xe
            MessageBox.Show("Thanh toán thành công! Xe của bạn đã được đặt.",
                           "Thành công",
                           MessageBoxButton.OK,
                           MessageBoxImage.Information);

            // Đóng cửa sổ sau khi hoàn tất
            this.Close();
        }

        private bool ValidateForm()
        {
            // Kiểm tra thông tin cá nhân
            if (string.IsNullOrWhiteSpace(txtName.Text) || txtName.Text == txtName.Tag.ToString())
            {
                MessageBox.Show("Vui lòng nhập tên của bạn.", "Thông tin không đầy đủ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text) || txtPhoneNumber.Text == txtPhoneNumber.Tag.ToString())
            {
                MessageBox.Show("Vui lòng nhập số điện thoại của bạn.", "Thông tin không đầy đủ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text) || txtAddress.Text == txtAddress.Tag.ToString())
            {
                MessageBox.Show("Vui lòng nhập địa chỉ của bạn.", "Thông tin không đầy đủ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCity.Text) || txtCity.Text == txtCity.Tag.ToString())
            {
                MessageBox.Show("Vui lòng nhập thành phố của bạn.", "Thông tin không đầy đủ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Kiểm tra thông tin thuê xe
            if (cmbPickupLocation.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn địa điểm nhận xe.", "Thông tin không đầy đủ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (dpPickupDate.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày nhận xe.", "Thông tin không đầy đủ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (cmbPickupTime.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn giờ nhận xe.", "Thông tin không đầy đủ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (cmbDropoffLocation.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn địa điểm trả xe.", "Thông tin không đầy đủ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (dpDropoffDate.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày trả xe.", "Thông tin không đầy đủ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (cmbDropoffTime.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn giờ trả xe.", "Thông tin không đầy đủ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Kiểm tra thông tin thanh toán
            if (rbCreditCard.IsChecked == true)
            {
                if (string.IsNullOrWhiteSpace(txtCardNumber.Text) || txtCardNumber.Text == txtCardNumber.Tag.ToString() ||
                    txtCardNumber.Text.Replace(" ", "").Length < 16)
                {
                    MessageBox.Show("Vui lòng nhập số thẻ tín dụng hợp lệ.", "Thông tin không đầy đủ", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtExpirationDate.Text) || txtExpirationDate.Text == txtExpirationDate.Tag.ToString() ||
                    txtExpirationDate.Text.Length < 5)
                {
                    MessageBox.Show("Vui lòng nhập ngày hết hạn thẻ hợp lệ.", "Thông tin không đầy đủ", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtCardHolder.Text) || txtCardHolder.Text == txtCardHolder.Tag.ToString())
                {
                    MessageBox.Show("Vui lòng nhập tên chủ thẻ.", "Thông tin không đầy đủ", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtCVC.Password) || txtCVC.Password.Length < 3)
                {
                    MessageBox.Show("Vui lòng nhập mã CVC hợp lệ.", "Thông tin không đầy đủ", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            // Kiểm tra điều khoản và điều kiện
            if (chkTerms.IsChecked != true)
            {
                MessageBox.Show("Vui lòng đồng ý với điều khoản và điều kiện để tiếp tục.", "Điều khoản và điều kiện", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
