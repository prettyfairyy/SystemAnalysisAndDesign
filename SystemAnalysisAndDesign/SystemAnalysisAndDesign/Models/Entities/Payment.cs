using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemAnalysisAndDesign.Models.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int RentalId { get; set; }

        public DateTime? PaymentDate { get; set; }
        public decimal? PaymentAmount { get; set; }
        public string? PaymentMethod { get; set; }

        // Navigation property
        public Rental Rental { get; set; }
    }
}
