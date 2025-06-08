using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemAnalysisAndDesign.Models.Entities
{
    public class UserAccount
    {
        public string Username { get; set; }

        public string PasswordHash { get; set; }
        public string Role { get; set; } // customer, admin, employee
        public string? Email { get; set; }
        public bool IsActive { get; set; }

        public Customer? Customer { get; set; }
        public Employee? Employee { get; set; }
    }

}
