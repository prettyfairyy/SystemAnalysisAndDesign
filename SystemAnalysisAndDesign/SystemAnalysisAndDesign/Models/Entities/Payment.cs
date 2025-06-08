using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemAnalysisAndDesign.Models.Entities
{
    public class Payment
    {
        public string PaymentId { get; set; }
        public string RentalId { get; set; }

        public DateTime? PaymentDate { get; set; }
        public decimal? PaymentAmount { get; set; }
        public string? PaymentMethod { get; set; }
        public bool? IsPaid { get; set; }


        // Navigation property
        public Rental Rental { get; set; }
        public static string GenerateNextId(int lastNumber)
        {
            return $"PM{(lastNumber + 1).ToString("D3")}";
        }
    }
}
