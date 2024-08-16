using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IlterisDictionaryLibrary.ViewModels
{
    public class VmBase
    {

        private bool _IsNotifyPropertyChangedActive;

        public bool IsNotifyPropertyChangedActive
        {
            get => _IsNotifyPropertyChangedActive;
            set
            {
                _IsNotifyPropertyChangedActive = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (IsNotifyPropertyChangedActive)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}