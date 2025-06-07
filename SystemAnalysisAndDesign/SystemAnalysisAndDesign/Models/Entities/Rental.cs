using System.ComponentModel;
using System.Runtime.CompilerServices;
using SystemAnalysisAndDesign.Models.Entities;

public class Rental : INotifyPropertyChanged
{
    private string _rentalStatus;
    private string _processedBy;
    public string RentalId { get; set; }

    public string CustomerId { get; set; }
    public string CarId { get; set; }
    public string RentalStatus
    {
        get => _rentalStatus;
        set
        {
            _rentalStatus = value;
            OnPropertyChanged();
        }
    }

    public string? ProcessedBy
    {
        get => _processedBy;
        set
        {
            _processedBy = value;
            OnPropertyChanged();
        }
    }

    public string? PickUpLocation { get; set; }
    public DateTime? PickUpDate { get; set; }
    public TimeSpan? PickUpTime { get; set; }
    public string? DropOffLocation { get; set; }
    public DateTime? DropOffDate { get; set; }
    public TimeSpan? DropOffTime { get; set; }
    public DateTime IssuedDate { get; set; } = DateTime.Now; // auto set lúc tạo

    public decimal? TotalAmount { get; set; }

    // Navigation properties
    public Customer Customer { get; set; }
    public Car Car { get; set; }
    public Employee? Employee { get; set; }  // For ProcessedBy
    public ICollection<Payment> Payments { get; set; }

    public static string GenerateNextId(int lastNumber)
    {
        return $"RE{(lastNumber + 1):D3}";
    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
