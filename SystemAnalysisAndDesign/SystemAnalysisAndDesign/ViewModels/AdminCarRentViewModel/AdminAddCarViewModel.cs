using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SystemAnalysisAndDesign.Models;
using SystemAnalysisAndDesign.Models.Entities;

namespace SystemAnalysisAndDesign.ViewModels.AdminCarRentViewModel
{
    internal class AdminAddCarViewModel : INotifyPropertyChanged
    {
        private readonly RentalDbContext _context;
        private readonly AdminCarListViewModel _carListViewModel;

        public AdminAddCarViewModel(RentalDbContext context, AdminCarListViewModel carListViewModel)
        {
            _context = context;
            _carListViewModel = carListViewModel;

            SaveCommand = new RelayCommand(_ => SaveCar());
            UploadImageCommand = new RelayCommand(_ => UploadImage());
        }

        public ICommand SaveCommand { get; }
        public ICommand UploadImageCommand { get; }

        // Properties
        private string _licensePlate;
        public string LicensePlate { get => _licensePlate; set { _licensePlate = value; OnPropertyChanged(nameof(LicensePlate)); } }

        private string _brand;
        public string Brand { get => _brand; set { _brand = value; OnPropertyChanged(nameof(Brand)); } }

        private string _model;
        public string Model { get => _model; set { _model = value; OnPropertyChanged(nameof(Model)); } }

        private int? _manufactureYear;
        public int? ManufactureYear { get => _manufactureYear; set { _manufactureYear = value; OnPropertyChanged(nameof(ManufactureYear)); } }

        private decimal? _pricePerDay;
        public decimal? PricePerDay { get => _pricePerDay; set { _pricePerDay = value; OnPropertyChanged(nameof(PricePerDay)); } }

        private int? _mileage;
        public int? Mileage { get => _mileage; set { _mileage = value; OnPropertyChanged(nameof(Mileage)); } }

        private string _carType;
        public string CarType { get => _carType; set { _carType = value; OnPropertyChanged(nameof(CarType)); } }

        private int? _capacity;
        public int? Capacity { get => _capacity; set { _capacity = value; OnPropertyChanged(nameof(Capacity)); } }

        private string _color;
        public string Color { get => _color; set { _color = value; OnPropertyChanged(nameof(Color)); } }

        private string _description;
        public string Description { get => _description; set { _description = value; OnPropertyChanged(nameof(Description)); } }

        private string _imagePath;
        public string ImagePath { get => _imagePath; set { _imagePath = value; OnPropertyChanged(nameof(ImagePath)); } }

        private string _imageFullPath;
        public string ImageFullPath { get => _imageFullPath; set { _imageFullPath = value; OnPropertyChanged(nameof(ImageFullPath)); } }

        private string _newFileName; // thêm vào class
        private string _destinationPath; // thêm vào class

        private void UploadImage()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                string relativeFolder = @"Image\CarImage";
                string destinationFolder = Path.Combine(projectPath, relativeFolder);

                if (!Directory.Exists(destinationFolder))
                    Directory.CreateDirectory(destinationFolder);

                string safeBrand = Brand?.Replace(" ", "").Replace("/", "-") ?? "Unknown";
                string safeModel = Model?.Replace(" ", "").Replace("/", "-") ?? "Unknown";
                _newFileName = $"{safeBrand}-{safeModel}.png";

                _destinationPath = Path.Combine(destinationFolder, _newFileName);

                GC.Collect();
                GC.WaitForPendingFinalizers();

                File.Copy(openFileDialog.FileName, _destinationPath, true);

                // ✅ Copy sang bin/Debug/... để UI binding từ ImagePath hiển thị được
                string runtimeImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", "CarImage");
                if (!Directory.Exists(runtimeImagePath))
                    Directory.CreateDirectory(runtimeImagePath);
                File.Copy(_destinationPath, Path.Combine(runtimeImagePath, _newFileName), true);

                ImageFullPath = _destinationPath;
                ImagePath = $"/Image/CarImage/{_newFileName}";
            }
        }


        private string GenerateCarId()
        {
            int count = _context.Cars.Count();
            return $"CAR{(count + 1).ToString("D3")}";
        }

        private void SaveCar()
        {
            var newCar = new Car
            {
                CarId = GenerateCarId(),
                LicensePlate = LicensePlate,
                Brand = Brand,
                Model = Model,
                ManufactureYear = ManufactureYear,
                PricePerDay = PricePerDay,
                Mileage = Mileage,
                CarType = CarType,
                Capacity = Capacity,
                Color = Color,
                Description = Description,
                ImagePath = ImagePath,
                CarStatus = "available"
            };

            try
            {
                _context.Cars.Add(newCar);
                _context.SaveChanges();

                // ✅ Không add trực tiếp, mà gọi reload lại toàn bộ từ database:
                _carListViewModel.AllCars = new ObservableCollection<Car>(_context.Cars.ToList());
                _carListViewModel.OnPropertyChanged(nameof(_carListViewModel.AllCars));

                ClearForm();
                MessageBox.Show("Car saved successfully!", "Success");
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Error saving car: {ex.InnerException?.Message ?? ex.Message}", "Error");
            }
        }



        private void ClearForm()
        {
            LicensePlate = string.Empty;
            Brand = string.Empty;
            Model = string.Empty;
            ManufactureYear = null;
            PricePerDay = null;
            Mileage = null;
            CarType = string.Empty;
            Capacity = null;
            Color = string.Empty;
            Description = string.Empty;
            ImagePath = string.Empty;
            ImageFullPath = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
