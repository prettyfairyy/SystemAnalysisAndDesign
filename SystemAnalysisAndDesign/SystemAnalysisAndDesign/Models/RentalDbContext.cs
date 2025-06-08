using Microsoft.EntityFrameworkCore;
using SystemAnalysisAndDesign.Models.Entities;

namespace SystemAnalysisAndDesign.Models
{
    public class RentalDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<UserAccount> UserAccount { get; set; }

        public RentalDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=CarRental;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CUSTOMER
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Account)
                .WithOne(a => a.Customer)
                .HasForeignKey<Customer>(c => c.Username);

            // EMPLOYEE
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeId);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Account)
                .WithOne(a => a.Employee)
                .HasForeignKey<Employee>(e => e.Username);

            // CAR
            modelBuilder.Entity<Car>()
                .HasKey(c => c.CarId);

            // RENTAL
            modelBuilder.Entity<Rental>()
                .HasKey(r => r.RentalId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CarId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Employee)
                .WithMany(e => e.ProcessedRentals)
                .HasForeignKey(r => r.ProcessedBy)
                .OnDelete(DeleteBehavior.SetNull); // nếu nhân viên bị xóa vẫn giữ Rental

            // PAYMENT
            modelBuilder.Entity<Payment>()
                .HasKey(p => p.PaymentId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Rental)
                .WithMany(r => r.Payments)
                .HasForeignKey(p => p.RentalId);

            // USER ACCOUNT
            modelBuilder.Entity<UserAccount>()
                .HasKey(a => a.Username);
        }
    }
}
