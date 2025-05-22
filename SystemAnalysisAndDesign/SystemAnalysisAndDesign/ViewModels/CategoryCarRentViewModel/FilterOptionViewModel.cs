using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SystemAnalysisAndDesign.Models;
using SystemAnalysisAndDesign.Models.DTOs;
using SystemAnalysisAndDesign.Models.Entities;
using SystemAnalysisAndDesign.Views.DetailCarRentView;

namespace SystemAnalysisAndDesign.ViewModels.CategoryCarRentViewModel
{
    public class FilterOptionViewModel : INotifyPropertyChanged
    {
        private readonly RentalDbContext _context;
        public ObservableCollection<FilterOption> CarTypes { get; set; }
        public ObservableCollection<FilterOption> Capacities { get; set; }
        public ObservableCollection<Car> AllCars { get; set; }
        public ObservableCollection<Car> FilteredCars { get; set; }

        private double _maxPrice;
        public double MaxPrice
        {
            get => _maxPrice;
            set
            {
                if (_maxPrice != value)
                {
                    _maxPrice = value;
                    OnPropertyChanged(nameof(MaxPrice));
                    UpdateFilteredCars();
                }
            }
        }

        //public FilterOptionViewModel(RentalDbContext context)
        //{
            //_context = context;

            //LoadDataFromDatabase();
        //}

        private void LoadDataFromDatabase()
        {
            // 1. Load tất cả xe từ DB
            AllCars = new ObservableCollection<Car>(_context.Cars.ToList());

            // 2. Tạo danh sách bộ lọc Type từ dữ liệu
            CarTypes = new ObservableCollection<FilterOption>(
                AllCars.GroupBy(car => car.CarType)
                       .Select(group => new FilterOption(group.Key, group.Count(), true))
            );

            // 3. Tạo danh sách bộ lọc Capacity từ dữ liệu
            Capacities = new ObservableCollection<FilterOption>(
                AllCars.GroupBy(car => car.Capacity)
                       .Select(group => new FilterOption(group.Key.ToString(), group.Count(), true))
            );

            // 4. Gán giá cao nhất từ danh sách
            MaxPrice = Convert.ToDouble(AllCars.Max(car => car.PricePerDay));

            // 5. Gán sự kiện khi checkbox thay đổi
            foreach (var option in CarTypes)
                option.PropertyChanged += (s, e) => UpdateFilteredCars();

            foreach (var option in Capacities)
                option.PropertyChanged += (s, e) => UpdateFilteredCars();

            // 6. Lọc lần đầu tiên
            UpdateFilteredCars();
        }

        private void UpdateFilteredCars()
        {
            var selectedTypes = CarTypes.Where(t => t.IsSelected).Select(t => t.Name).ToList();
            var selectedCaps = Capacities.Where(c => c.IsSelected).Select(c => c.Name).ToList();

            var result = AllCars.Where(car =>
                selectedTypes.Contains(car.CarType) &&
                selectedCaps.Contains(car.Capacity.ToString()) &&
                car.PricePerDay.HasValue &&
                Convert.ToDouble(car.PricePerDay.Value) <= MaxPrice
            );


            FilteredCars = new ObservableCollection<Car>(result);
            OnPropertyChanged(nameof(FilteredCars));
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand RentCommand { get; }

        public FilterOptionViewModel(RentalDbContext context)
        {
            _context = context;

            RentCommand = new RelayCommand<Car>(car =>
            {
                if (car == null) return;
                var detailView = new DetailCarRentMainView(car, this);
                detailView.Show();
            });

            LoadDataFromDatabase();
        }

    }
}
