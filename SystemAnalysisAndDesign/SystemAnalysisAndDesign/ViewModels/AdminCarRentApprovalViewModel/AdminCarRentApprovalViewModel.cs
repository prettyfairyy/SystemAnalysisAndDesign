using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using SystemAnalysisAndDesign.Models;
using SystemAnalysisAndDesign.Models.Entities;
using SystemAnalysisAndDesign.ViewModels.Stores;

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
            ApproveCommand = new RelayCommand<Rental>(ApproveRequest, CanApproveOrReject);
            RejectCommand = new RelayCommand<Rental>(RejectRequest, CanApproveOrReject);

            LoadAllRentalsFromDatabase();
        }

        private void LoadAllRentalsFromDatabase()
        {
            using (var context = new RentalDbContext())
            {
                var rentals = context.Rentals
                    .Include(r => r.Car)
                    .Include(r => r.Customer)
                    .OrderBy(r =>
                        r.RentalStatus == "waiting" ? 0 :
                        r.RentalStatus == "approved" ? 1 : 2) // Sắp xếp theo trạng thái
                    .ThenByDescending(r => r.IssuedDate)     // Sắp theo ngày gần nhất
                    .ToList();

                PendingApprovals = new ObservableCollection<Rental>(rentals);
            }
        }

        private bool CanApproveOrReject(Rental rental)
        {
            return rental != null && rental.RentalStatus == "waiting";
        }

        private void ApproveRequest(Rental item)
        {
            Debug.WriteLine($"Approving: {item.RentalId}");
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
                    using (var context = new RentalDbContext())
                    {
                        var rentalInDb = context.Rentals.FirstOrDefault(r => r.RentalId == item.RentalId);
                        if (rentalInDb != null)
                        {
                            rentalInDb.RentalStatus = "approved";
                            if (LoginStore.CurrentEmployee != null)
                                rentalInDb.ProcessedBy = LoginStore.CurrentEmployee.EmployeeId; ;
                            OnPropertyChanged(nameof(PendingApprovals));
                            context.SaveChanges();

                            // Cập nhật lại dữ liệu cho đối tượng đang binding trong UI
                            item.RentalStatus = "approved";
                            item.ProcessedBy = rentalInDb.ProcessedBy;
                        }
                    }

                    MessageBox.Show($"Đã phê duyệt đơn thuê {item.RentalId}.", "Thành công");
                    CommandManager.InvalidateRequerySuggested();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi phê duyệt:\n{ex.InnerException?.Message ?? ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    using (var context = new RentalDbContext())
                    {
                        var rentalInDb = context.Rentals.FirstOrDefault(r => r.RentalId == item.RentalId);
                        if (rentalInDb != null)
                        {
                            rentalInDb.RentalStatus = "rejected";
                            if (LoginStore.CurrentEmployee != null)
                                rentalInDb.ProcessedBy = LoginStore.CurrentEmployee.EmployeeId;
                            context.SaveChanges();

                            // Cập nhật lại trong UI
                            item.RentalStatus = "rejected";
                            item.ProcessedBy = rentalInDb.ProcessedBy;
                        }
                    }

                    MessageBox.Show($"Yêu cầu thuê {item.RentalId} đã bị từ chối.", "Đã từ chối");
                    CommandManager.InvalidateRequerySuggested(); // để cập nhật trạng thái CanExecute
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi phê duyệt:\n{ex.InnerException?.Message ?? ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
        public class StatusToVisibilityConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                string status = value?.ToString();
                string expected = parameter?.ToString();
                return status == expected ? Visibility.Visible : Visibility.Collapsed;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

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
