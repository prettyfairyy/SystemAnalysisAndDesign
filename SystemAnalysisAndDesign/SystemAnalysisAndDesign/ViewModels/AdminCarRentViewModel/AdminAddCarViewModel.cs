using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SystemAnalysisAndDesign.Models.Entities;
using SystemAnalysisAndDesign.Models;
using System.Windows;
using System.IO;

namespace SystemAnalysisAndDesign.ViewModels.AdminCarRentViewModel
{
    internal class AdminAddCarViewModel : INotifyPropertyChanged
    {
        private readonly RentalDbContext _context;
        private readonly AdminCarListViewModel _carListViewModel;

        // Các property binding với View
        private string _licensePlate;
        public string LicensePlate
        {
            get => _licensePlate;
            set { _licensePlate = value; OnPropertyChanged(nameof(LicensePlate)); }
        }

        private string _brand;
        public string Brand
        {
            get => _brand;
            set { _brand = value; OnPropertyChanged(nameof(Brand)); }
        }

        private string _model;
        public string Model
        {
            get => _model;
            set { _model = value; OnPropertyChanged(nameof(Model)); }
        }

        private int? _manufactureYear;
        public int? ManufactureYear
        {
            get => _manufactureYear;
            set { _manufactureYear = value; OnPropertyChanged(nameof(ManufactureYear)); }
        }

        private decimal? _pricePerDay;
        public decimal? PricePerDay
        {
            get => _pricePerDay;
            set { _pricePerDay = value; OnPropertyChanged(nameof(PricePerDay)); }
        }

        private int? _mileage;
        public int? Mileage
        {
            get => _mileage;
            set { _mileage = value; OnPropertyChanged(nameof(Mileage)); }
        }

        private string _carType;
        public string CarType
        {
            get => _carType;
            set { _carType = value; OnPropertyChanged(nameof(CarType)); }
        }

        private int? _capacity;
        public int? Capacity
        {
            get => _capacity;
            set { _capacity = value; OnPropertyChanged(nameof(Capacity)); }
        }

        private string _color;
        public string Color
        {
            get => _color;
            set { _color = value; OnPropertyChanged(nameof(Color)); }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set { _imagePath = value; OnPropertyChanged(nameof(ImagePath)); }
        }

        // Command xử lý
        public ICommand SaveCommand { get; }
        public ICommand UploadImageCommand { get; }

        public AdminAddCarViewModel(RentalDbContext context, AdminCarListViewModel carListViewModel)
        {
            _context = context;
            _carListViewModel = carListViewModel;

            SaveCommand = new RelayCommand(_ => SaveCar());
            UploadImageCommand = new RelayCommand(_ => UploadImage());
        }

        private void UploadImage()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string projectBasePath = AppDomain.CurrentDomain.BaseDirectory;
                string destinationFolder = System.IO.Path.Combine(projectBasePath, "Image", "CarImage");

                if (!Directory.Exists(destinationFolder))
                    Directory.CreateDirectory(destinationFolder);

                // Đảm bảo tên brand/model không có ký tự đặc biệt
                string safeBrand = Brand?.Replace(" ", "").Replace("/", "-") ?? "Unknown";
                string safeModel = Model?.Replace(" ", "").Replace("/", "-") ?? "Unknown";
                string newFileName = $"{safeBrand}-{safeModel}.png";

                string destinationPath = System.IO.Path.Combine(destinationFolder, newFileName);

                File.Copy(openFileDialog.FileName, destinationPath, true);

                // Lưu đường dẫn tương đối để insert vào DB
                ImagePath = $"/Image/CarImage/{newFileName}";
            }
        }
        private string GenerateCarId()
        {
            int existingCount = _context.Cars.Count();
            return $"CAR{(existingCount + 1).ToString("D3")}";
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
                _carListViewModel.AllCars.Add(newCar);
                ClearForm();

                System.Windows.MessageBox.Show("Car saved successfully!", "Success");
            }
            catch (DbUpdateException ex)
            {
                System.Windows.MessageBox.Show($"Error saving car: {ex.InnerException?.Message ?? ex.Message}", "Error");
            }
        }

        private void ClearForm()
        {
            // Reset tất cả property
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
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
