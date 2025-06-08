using System.ComponentModel;
using SystemAnalysisAndDesign.Models.Entities;

namespace SystemAnalysisAndDesign.ViewModels.PaymentViewModel
{
    public class PaymentViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _phone;
        private string _address;
        private string _town;

        public string FullName
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(FullName)); }
        }

        public string Phone
        {
            get => _phone;
            set { _phone = value; OnPropertyChanged(nameof(Phone)); }
        }

        public string FullAddress
        {
            get => _address;
            set { _address = value; OnPropertyChanged(nameof(FullAddress)); }
        }

        public string Town
        {
            get => _town;
            set { _town = value; OnPropertyChanged(nameof(Town)); }
        }

        public PaymentViewModel(Customer customer)
        {
            FullName = customer.FullName;
            Phone = customer.Phone;
            FullAddress = customer.FullAddress;
            Town = customer.Town;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
