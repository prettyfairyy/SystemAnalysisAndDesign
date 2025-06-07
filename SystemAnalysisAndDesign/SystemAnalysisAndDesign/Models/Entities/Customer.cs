using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemAnalysisAndDesign.Models.Entities
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string Username { get; set; }

        public string FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? DriverLicense { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public UserAccount Account { get; set; }
        public ICollection<Rental> Rentals { get; set; }

        public string Town
        {
            get
            {
                if (string.IsNullOrEmpty(Address)) return "";
                var parts = Address.Split(',');
                return parts.Length > 0 ? parts[^1].Trim() : "";
            }
        }

        public string FullAddress
        {
            get
            {
                if (string.IsNullOrEmpty(Address)) return "";
                var parts = Address.Split(',');
                return parts.Length > 1 ? string.Join(",", parts.Take(parts.Length - 1)).Trim() : Address;
            }
        }
    }
}
