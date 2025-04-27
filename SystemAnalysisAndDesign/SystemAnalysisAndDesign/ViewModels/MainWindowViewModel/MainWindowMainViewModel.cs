using System.Collections.ObjectModel;
using System.Linq;
using SystemAnalysisAndDesign.Models;
using SystemAnalysisAndDesign.Models.Entities;
using SystemAnalysisAndDesign.ViewModels;

namespace SystemAnalysisAndDesign.ViewModels.MainWindowViewModel
{
    public class MainWindowMainViewModel : ViewModelBase
    {
        private string _carImage;
        public string CarImage
        {
            get => _carImage;
            set => SetProperty(ref _carImage, value);
        }

        public ObservableCollection<Car> CarList { get; set; } // Toàn bộ xe
        public ObservableCollection<Car> Top3Cars { get; set; } // 4 xe nổi bật

        public MainWindowMainViewModel()
        {
            LoadCarsFromDatabase();
        }

        private void LoadCarsFromDatabase()
        {
            using (var context = new RentalDbContext())
            {
                var cars = context.Cars.ToList();
                CarList = new ObservableCollection<Car>(cars);
                Top3Cars = new ObservableCollection<Car>(cars.Take(3));
            }

            LoadRandomCarImage();
        }

        private void LoadRandomCarImage()
        {
            if (Top3Cars != null && Top3Cars.Count > 0)
            {
                Random random = new Random();
                int index = random.Next(Top3Cars.Count);
                CarImage = Top3Cars[index].ImagePath;
            }
        }
    }
}
