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
using Microsoft.Win32;

namespace SystemAnalysisAndDesign.Views.AdminCarRentView.ComponentAdminCarRentView
{
    /// <summary>
    /// Interaction logic for AdminAddCarView.xaml
    /// </summary>
    public partial class AdminAddCarView : UserControl
    {
        public AdminAddCarView()
        {
            InitializeComponent();
        }
        
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            // Mở hộp thoại chọn file ảnh :3
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                // Hiển thị ảnh lên imgDisplay :3
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                imgDisplay.Source = bitmap;
            }
        }
    }
}
