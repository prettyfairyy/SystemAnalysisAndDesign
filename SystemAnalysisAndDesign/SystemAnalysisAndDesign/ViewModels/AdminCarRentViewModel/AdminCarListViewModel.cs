using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SystemAnalysisAndDesign.Models;
using SystemAnalysisAndDesign.Models.DTOs;
using SystemAnalysisAndDesign.Models.Entities;

namespace SystemAnalysisAndDesign.ViewModels.AdminCarRentViewModel
{
    public class AdminCarListViewModel
    {
        private readonly RentalDbContext _context;
        public ObservableCollection<Car> AllCars { get; set; }
        public ICommand DeleteCarCommand { get; }
        private readonly AdminCarListViewModel _carListViewModel;
        public AdminCarListViewModel(RentalDbContext context)
        {
            _context = context;

            LoadDataFromDatabase();
            DeleteCarCommand = new RelayCommand<Car>(DeleteCar);
        }
        public void LoadDataFromDatabase()
        {
            AllCars = new ObservableCollection<Car>(_context.Cars.ToList());
            OnPropertyChanged(nameof(AllCars));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void DeleteCar(Car car)
        {
            if (car == null) return;

            try
            {
                // Xóa khỏi database
                _context.Cars.Remove(car);
                _context.SaveChanges();

                // Xóa khỏi ObservableCollection (UI tự cập nhật)
                _carListViewModel.AllCars.Remove(car);
            }
            catch (DbUpdateException ex)
            {
                // Xử lý lỗi database
                MessageBox.Show($"Database error: {ex.InnerException?.Message}");
            }
        }
    }
}
