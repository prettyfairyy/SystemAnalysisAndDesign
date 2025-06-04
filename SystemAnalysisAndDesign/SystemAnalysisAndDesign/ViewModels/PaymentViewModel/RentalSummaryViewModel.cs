using CommunityToolkit.Mvvm.ComponentModel;
using System;
using SystemAnalysisAndDesign.Models;
using SystemAnalysisAndDesign.Models.Entities;

namespace SystemAnalysisAndDesign.ViewModels.PaymentViewModel
{
    public partial class RentalSummaryViewModel : ObservableObject
    {
        private readonly RentalInfoViewModel rentalInfoViewModel;
        public Car? SelectedCar => SelectedCarStore.SelectedCar;

        private decimal totalAmount;
        public decimal TotalAmount
        {
            get => totalAmount;
            set => SetProperty(ref totalAmount, value);
        }


        public RentalSummaryViewModel(RentalInfoViewModel rentalInfo)
        {
            rentalInfoViewModel = rentalInfo;

            // Đăng ký sự kiện khi ngày thay đổi
            rentalInfoViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(RentalInfoViewModel.PickUpDate) ||
                    e.PropertyName == nameof(RentalInfoViewModel.DropOffDate))
                {
                    CalculateTotalAmount();
                }
            };

            CalculateTotalAmount();
        }

        public void CalculateTotalAmount()
        {
            var car = SelectedCarStore.SelectedCar;
            var pickUp = rentalInfoViewModel.PickUpDate;
            var dropOff = rentalInfoViewModel.DropOffDate;

            if (car == null || pickUp == null || dropOff == null)
            {
                TotalAmount = 0; // reset if invalid
                return;
            }

            int numDays = (dropOff.Value - pickUp.Value).Days;
            if (numDays <= 0)
            {
                TotalAmount = 0;
                return;
            }

            TotalAmount = numDays * (car.PricePerDay ?? 0);
            System.Diagnostics.Debug.WriteLine($"[DEBUG] TotalAmount = {totalAmount}");

        }
    }
}
