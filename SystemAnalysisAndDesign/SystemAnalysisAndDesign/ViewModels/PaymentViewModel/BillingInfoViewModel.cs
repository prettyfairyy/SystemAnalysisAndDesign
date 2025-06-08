using System.ComponentModel;
using System.Runtime.CompilerServices;
using SystemAnalysisAndDesign.Models.Entities;
using SystemAnalysisAndDesign.ViewModels.Stores;

namespace SystemAnalysisAndDesign.ViewModels.PaymentViewModel
{
    public class BillingInfoViewModel : INotifyPropertyChanged
    {
        private string _fullName;
        private string _phone;
        private string _fullAddress;
        private string _town;

        public string FullName
        {
            get => _fullName;
            set { _fullName = value; OnPropertyChanged(); }
        }

        public string Phone
        {
            get => _phone;
            set { _phone = value; OnPropertyChanged(); }
        }

        public string FullAddress
        {
            get => _fullAddress;
            set { _fullAddress = value; OnPropertyChanged(); }
        }

        public string Town
        {
            get => _town;
            set { _town = value; OnPropertyChanged(); }
        }

        public BillingInfoViewModel()
        {
            // Lấy thông tin từ LoginStore
            Customer customer = LoginStore.CurrentCustomer;

            if (customer != null)
            {
                FullName = customer.FullName;
                Phone = customer.Phone;
                FullAddress = customer.FullAddress;
                Town = customer.Town;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
