using System.ComponentModel;
using System.Runtime.CompilerServices;
using ReactiveUI;

namespace Simple_PMS.ViewModels
{
    public class ViewModelBase : ReactiveObject, INotifyPropertyChanged
    {
        new public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
