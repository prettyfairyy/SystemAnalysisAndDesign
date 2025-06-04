using System.Collections.ObjectModel; // Để dùng ObservableCollection
using System.Windows.Input;           // Để dùng ICommand
using SystemAnalysisAndDesign.Models.Entities; // Để dùng Car
using SystemAnalysisAndDesign.ViewModels.Base; // Để dùng ViewModelBase
using SystemAnalysisAndDesign.ViewModels;      // Để dùng RelayCommand
using System.Linq;                         // Để dùng LINQ (ví dụ: Take)
using System.Windows;                      // Để dùng MessageBox (ví dụ)

namespace SystemAnalysisAndDesign.ViewModels.MainWindowMainViewModel // Điều chỉnh namespace nếu cần
{
    public class MainWindowMainViewModel : ViewModelBase
    {
        // --- Properties for Data Binding ---

        private ObservableCollection<Car> _featuredCars = new ObservableCollection<Car>();
        public ObservableCollection<Car> FeaturedCars
        {
            get => _featuredCars;
            set => SetProperty(ref _featuredCars, value);
        }

        // Bạn cũng có thể thêm các thuộc tính khác nếu cần binding
        // Ví dụ: Danh sách các Brands
        private ObservableCollection<string> _brands = new ObservableCollection<string>();
        public ObservableCollection<string> Brands
        {
            get => _brands;
            set => SetProperty(ref _brands, value);
        }

        // --- Commands ---

        public ICommand RentCarCommand { get; }
        public ICommand NavigateToRentalCommand { get; } // Cho nút "Rental Car" ở trên

        // --- Constructor ---

        public MainWindowMainViewModel()
        {
            // Khởi tạo Commands
            RentCarCommand = new RelayCommand(ExecuteRentCar, CanExecuteRentCar);
            NavigateToRentalCommand = new RelayCommand(ExecuteNavigateToRental);

            // Tải dữ liệu ban đầu
            LoadInitialData();
        }

        // --- Command Methods ---

        private void ExecuteRentCar(object? parameter)
        {
            if (parameter is Car selectedCar)
            {
                // Xử lý logic khi nhấn nút Rent
                MessageBox.Show($"Đã chọn thuê xe: {selectedCar.Brand} {selectedCar.Model} (ID: {selectedCar.CarId})");
                // TODO: Triển khai logic chuyển trang hoặc hiển thị dialog đặt xe
                // Ví dụ: NavigationService.Navigate(new RentalPage(selectedCar));
            }
        }

        private bool CanExecuteRentCar(object? parameter)
        {
            // Điều kiện để nút Rent có thể nhấn (ví dụ: xe phải available)
            // Nếu không có điều kiện gì đặc biệt, cứ trả về true
            if (parameter is Car car)
            {
                // return car.CarStatus?.ToLower() == "available"; // Ví dụ
            }
            return true; // Mặc định là luôn có thể nhấn
        }

        private void ExecuteNavigateToRental(object? parameter)
        {
             // Xử lý logic khi nhấn nút "Rental Car" ở card trên cùng
             MessageBox.Show("Chuyển đến trang danh sách xe cho thuê!");
             // TODO: Triển khai logic chuyển trang
        }


        // --- Data Loading ---

        private void LoadInitialData()
        {
            // *** Quan trọng: Đây là nơi bạn sẽ gọi Service/Repository để lấy dữ liệu thật ***
            // Ví dụ:
            // using (var dbContext = new RentalDbContext()) // Không nên dùng DbContext trực tiếp ở đây trong thực tế
            // {
            //     var carsFromDb = dbContext.Cars.Take(3).ToList(); // Lấy 3 xe đầu tiên
            //     FeaturedCars = new ObservableCollection<Car>(carsFromDb);
            //
            //     var brandsFromDb = dbContext.Cars.Select(c => c.Brand).Distinct().ToList();
            //     Brands = new ObservableCollection<string>(brandsFromDb);
            // }

            // *** Dữ liệu giả lập để Demo ***
            // (Hãy đảm bảo các đường dẫn ImagePath tồn tại hoặc thay thế bằng ảnh mẫu)
            FeaturedCars.Add(new Car { CarId = "C001", Brand = "Audi", Model = "Q8", PricePerDay = 1000000, Capacity = 4, ImagePath = "/Image/CarImage/Audi-Q8.png", /* Thêm các thuộc tính khác nếu cần */ });
            FeaturedCars.Add(new Car { CarId = "C002", Brand = "BMW", Model = "X5", PricePerDay = 1200000, Capacity = 5, ImagePath = "/Image/CarImage/Audi-Q8.png", /* Thêm các thuộc tính khác nếu cần */ });
            FeaturedCars.Add(new Car { CarId = "C003", Brand = "Audi", Model = "A8", PricePerDay = 1500000, Capacity = 5, ImagePath = "/Image/CarImage/Audi-Q8.png", /* Thêm các thuộc tính khác nếu cần */ });

            // Dữ liệu giả lập cho Brands
            Brands.Add("Audi");
            Brands.Add("Mercedes");
            Brands.Add("Land Rover");
            Brands.Add("Ferrari");
            Brands.Add("Tesla");
        }
    }
}
