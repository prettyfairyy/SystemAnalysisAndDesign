using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemAnalysisAndDesign.Models.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? DriverLicense { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public ICollection<Rental> Rentals { get; set; }
    }
}
