// Trong thư mục ViewModels hoặc một thư mục Core/Base nào đó
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SystemAnalysisAndDesign.ViewModels.Base // Điều chỉnh namespace nếu cần
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Helper method để set giá trị và gọi OnPropertyChanged nếu giá trị thay đổi
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}