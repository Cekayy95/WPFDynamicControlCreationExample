using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DynamicControlCreationExample.WPFMVVM.ViewModel;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
    {
        if (backingField!.Equals(value))
            return;
        backingField = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void RaisePropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}