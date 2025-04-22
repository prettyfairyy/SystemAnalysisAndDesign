using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SystemAnalysisAndDesign.Models.Entities
{
    public class Car
    {
        public string CarId { get; set; }
        public string LicensePlate { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int? ManufactureYear { get; set; }
        public string? CarStatus { get; set; } // available, rented, maintenance
        public decimal? PricePerDay { get; set; }
        public int? Mileage { get; set; }
        public string? CarType { get; set; } // SUV, Sedan, etc.
        public int? Capacity { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<Rental> Rentals { get; set; }
        public SolidColorBrush StatusColor
        {
            get
            {
                return CarStatus?.ToLower() switch
                {
                    "available" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#63A9E8")), // Blue
                    "maintenance" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F28B82")), // Red
                    "rented" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#81C784")), // Green
                    _ => new SolidColorBrush(Colors.Gray)
                };
            }
        }
    }
}
