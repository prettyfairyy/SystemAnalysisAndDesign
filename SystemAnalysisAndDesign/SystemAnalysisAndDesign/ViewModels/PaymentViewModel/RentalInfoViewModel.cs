using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SystemAnalysisAndDesign.Models;

namespace SystemAnalysisAndDesign.ViewModels.PaymentViewModel
{
    public partial class RentalInfoViewModel : ObservableObject
    {
        [ObservableProperty] private DateTime? pickUpDate;
        [ObservableProperty] private DateTime? dropOffDate;
        [ObservableProperty] private string? pickUpLocation;
        [ObservableProperty] private string? dropOffLocation;
        [ObservableProperty] private TimeSpan? pickUpTime;
        [ObservableProperty] private TimeSpan? dropOffTime;

        partial void OnPickUpDateChanged(DateTime? value)
        {
            if (PickUpDate == null || DropOffDate == null) return;
            if (SelectedCarStore.SelectedCar == null) return;

            var context = new RentalDbContext();
            var carId = SelectedCarStore.SelectedCar.CarId;

            bool isOverlapping = context.Rentals.Any(r =>
                r.CarId == carId &&
                r.PickUpDate.HasValue && r.DropOffDate.HasValue &&
                !(DropOffDate < r.PickUpDate || PickUpDate > r.DropOffDate)
            );

            if (isOverlapping)
            {
                MessageBox.Show("Chiếc xe này đã được đặt vào ngày bạn chọn. Vui lòng chọn ngày khác.");
                PickUpDate = null;
            }
        }

        partial void OnDropOffDateChanged(DateTime? value)
        {
            if (PickUpDate == null || DropOffDate == null) return;
            if (SelectedCarStore.SelectedCar == null) return;

            var context = new RentalDbContext();
            var carId = SelectedCarStore.SelectedCar.CarId;

            bool isOverlapping = context.Rentals.Any(r =>
                r.CarId == carId &&
                r.PickUpDate.HasValue && r.DropOffDate.HasValue &&
                !(DropOffDate < r.PickUpDate || PickUpDate > r.DropOffDate)
            );

            if (isOverlapping)
            {
                MessageBox.Show("Chiếc xe này đã được đặt vào ngày bạn chọn. Vui lòng chọn ngày khác.");
                DropOffDate = null;
            }
        }
    }
}
