using System.ComponentModel;
using SystemAnalysisAndDesign.Models.Entities;

namespace SystemAnalysisAndDesign.ViewModels.DetailCarRentViewModel
{
    public class DetailCarViewModel : INotifyPropertyChanged
    {
        public Car SelectedCar { get; set; }

        public DetailCarViewModel(Car car)
        {
            SelectedCar = car;
            SelectedCar.Steering ??= "Manual";
            SelectedCar.Gasoline ??= "70L";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
