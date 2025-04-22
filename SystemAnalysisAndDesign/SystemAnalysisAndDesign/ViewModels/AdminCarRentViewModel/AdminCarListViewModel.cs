using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemAnalysisAndDesign.Models;
using SystemAnalysisAndDesign.Models.DTOs;
using SystemAnalysisAndDesign.Models.Entities;

namespace SystemAnalysisAndDesign.ViewModels.AdminCarRentViewModel
{
    internal class AdminCarListViewModel
    {
        private readonly RentalDbContext _context;
        public ObservableCollection<Car> AllCars { get; set; }
        public AdminCarListViewModel(RentalDbContext context)
        {
            _context = context;

            LoadDataFromDatabase();
        }
        private void LoadDataFromDatabase()
        {
            AllCars = new ObservableCollection<Car>(_context.Cars.ToList());
            OnPropertyChanged(nameof(AllCars));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
