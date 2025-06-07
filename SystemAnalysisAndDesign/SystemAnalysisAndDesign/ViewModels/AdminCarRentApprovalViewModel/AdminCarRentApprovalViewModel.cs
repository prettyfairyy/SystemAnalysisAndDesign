using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using SystemAnalysisAndDesign.Models;
using SystemAnalysisAndDesign.Models.Entities;

namespace SystemAnalysisAndDesign.ViewModels.AdminCarRentApprovalViewModel
{
    public class AdminCarRentApprovalViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Rental> _pendingApprovals;
        public ObservableCollection<Rental> PendingApprovals
        {
            get => _pendingApprovals;
            set
            {
                _pendingApprovals = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasPendingApprovals));
            }
        }

        public bool HasPendingApprovals => PendingApprovals != null && PendingApprovals.Count > 0;

        public ICommand ApproveCommand { get; }
        public ICommand RejectCommand { get; }

        public AdminCarRentApprovalViewModel()
        {
            ApproveCommand = new RelayCommand<Rental>(ApproveRequest);
            RejectCommand = new RelayCommand<Rental>(RejectRequest);

            LoadSampleData();
        }

        private void ApproveRequest(Rental item)
        {
            if (item == null) return;

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn PHÊ DUYỆT yêu cầu thuê xe của {item.Customer.FullName}?",
                "Xác nhận phê duyệt",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // TODO: Thực hiện logic phê duyệt thực tế ở đây
                    // carRentalService.ApproveRental(item.Id);

                    PendingApprovals.Remove(item);

                    MessageBox.Show(
                        $"Yêu cầu thuê xe {item.Car.Brand} {item.Car.Model} đã được phê duyệt.",
                        "Thành công",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    OnPropertyChanged(nameof(HasPendingApprovals));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Lỗi khi phê duyệt: {ex.Message}",
                        "Lỗi",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void RejectRequest(Rental item)
        {
            if (item == null) return;

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn TỪ CHỐI yêu cầu thuê xe của {item.Customer.FullName}?\nHành động này không thể hoàn tác.",
                "Xác nhận từ chối",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // TODO: Thực hiện logic từ chối thực tế ở đây
                    // carRentalService.RejectRental(item.Id);

                    PendingApprovals.Remove(item);

                    MessageBox.Show(
                        $"Yêu cầu thuê xe {item.Car.Brand} {item.Car.Model} đã bị từ chối.",
                        "Đã từ chối",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    OnPropertyChanged(nameof(HasPendingApprovals));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Lỗi khi từ chối: {ex.Message}",
                        "Lỗi",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void LoadSampleData()
        {
            // Dữ liệu mẫu cho mục đích thiết kế/test
            PendingApprovals = new ObservableCollection<Rental>
            {
                new Rental
                {
                    RentalId = "A1",
                    Car = new Car
                    {
                        CarId = "C001",
                        Brand = "Toyota",
                        Model = "Camry",
                        CarType = "Sedan",
                        LicensePlate = "29A-12345",
                        ImagePath = "/Image/CarImage/Audi-Q8.png"
                    },
                    Customer = new Customer
                    {
                        CustomerId = "C005",
                        FullName = "Nguyễn Văn A",
                        Email = "nguyenvana@example.com",
                        Phone = "0912345678",
                        DriverLicense = "B2-123456789"
                    },
                    PickUpDate = DateTime.Now.AddDays(1),
                    DropOffDate = DateTime.Now.AddDays(4),
                    RentalStatus = "Paid"
                }
            };
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    // RelayCommand<T> tự cài đặt, không phụ thuộc external package
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
