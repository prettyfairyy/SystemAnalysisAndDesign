using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemAnalysisAndDesign.Models.Entities
{
    public class Car
    {
        public int CarId { get; set; }
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

        public ICollection<Rental> Rentals { get; set; }
    }
}
