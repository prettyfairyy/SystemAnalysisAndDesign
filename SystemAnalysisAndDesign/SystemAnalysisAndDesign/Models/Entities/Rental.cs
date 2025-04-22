using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemAnalysisAndDesign.Models.Entities
{
    public class Rental
    {
        public string RentalId { get; set; }
        public string CustomerId { get; set; }
        public string CarId { get; set; }

        public string? PickUpLocation { get; set; }
        public DateTime? PickUpDate { get; set; }
        public TimeSpan? PickUpTime { get; set; }
        public string? DropOffLocation { get; set; }
        public DateTime? DropOffDate { get; set; }
        public TimeSpan? DropOffTime { get; set; }
        public string? RentalStatus { get; set; } // ongoing, completed, cancelled
        public decimal? TotalAmount { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
