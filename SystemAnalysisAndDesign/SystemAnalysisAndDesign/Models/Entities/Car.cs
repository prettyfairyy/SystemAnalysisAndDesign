using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;

namespace SystemAnalysisAndDesign.Models.Entities
{
    public class Car
    {
        public string CarId { get; set; }
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
        public string? ImagePath { get; set; }
        [NotMapped]
        public string Steering { get; set; } = "Manual";
        [NotMapped]
        public string Gasoline { get; set; } = "70L";

        public ICollection<Rental> Rentals { get; set; }
        public SolidColorBrush StatusColor
        {
            get
            {
                return CarStatus?.ToLower() switch
                {
                    "available" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#63A9E8")), // Blue
                    "maintenance" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F28B82")), // Red
                    "rented" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#81C784")), // Green
                    _ => new SolidColorBrush(Colors.Gray)
                };
            }
        }

        [NotMapped]
        public string ImageAbsolutePath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath)) return null;

                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string relativePath = ImagePath.TrimStart('/').Replace("/", "\\");
                string fullPath = System.IO.Path.Combine(basePath, relativePath);

                // 👇 Nếu ảnh chưa có trong bin, tự động copy từ thư mục source (project)
                if (!File.Exists(fullPath))
                {
                    try
                    {
                        // Đường dẫn ảnh gốc trong thư mục project (source)
                        string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                        string projectImagePath = Path.Combine(projectRoot, relativePath);

                        if (File.Exists(projectImagePath))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                            File.Copy(projectImagePath, fullPath, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Logging nếu cần
                        Console.WriteLine("Image copy failed: " + ex.Message);
                    }
                }

                return fullPath;
            }
        }


    }
}
