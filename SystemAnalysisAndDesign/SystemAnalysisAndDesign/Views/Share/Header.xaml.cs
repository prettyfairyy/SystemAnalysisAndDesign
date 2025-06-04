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

namespace SystemAnalysisAndDesign.Views.Share
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
        private void SettingIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SettingsPopup.IsOpen = !SettingsPopup.IsOpen;
        }
        
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Quay lại trang CategoryCarRentMainView
            Window parentWindow = Window.GetWindow(this);

            // Mở cửa sổ MainWindowMainView
            var mainWindow = new MainWindowView.MainWindowMainView();
            mainWindow.Show();

            // Đóng cửa sổ hiện tại
            parentWindow?.Close();
        }
    }
}
