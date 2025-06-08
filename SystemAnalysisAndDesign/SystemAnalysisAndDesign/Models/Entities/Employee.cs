using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemAnalysisAndDesign.Models.Entities
{
    public class Employee
    {
        public string EmployeeId { get; set; }
        public string Username { get; set; }

        public string FullName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
        public DateTime? HireDate { get; set; }

        public UserAccount Account { get; set; }

        public ICollection<Rental> ProcessedRentals { get; set; } // Rentals approved/rejected by this employee
    }

}
