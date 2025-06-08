using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemAnalysisAndDesign.Models.Entities;

namespace SystemAnalysisAndDesign.ViewModels.Stores
{
    public static class LoginStore
    {
        public static Customer? CurrentCustomer { get; set; }
        public static Employee? CurrentEmployee { get; set; }
    }
}
