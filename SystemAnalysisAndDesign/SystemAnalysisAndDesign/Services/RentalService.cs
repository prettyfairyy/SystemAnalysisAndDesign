using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemAnalysisAndDesign.Models.Entities;

namespace SystemAnalysisAndDesign.Services
{
    public class RentalService
    {
        public static void CreateRental(
            Customer customer,
            Car car,
            DateTime pickupDate,
            DateTime dropoffDate,
            string pickupLocation,
            string dropoffLocation,
            string paymentMethod)
        {
            // Check schedule, insert to DB, calculate amount
            // Logging, Notifications etc.
        }
    }
}
