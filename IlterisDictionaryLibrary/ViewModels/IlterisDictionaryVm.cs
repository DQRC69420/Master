using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IlterisDictionaryLibrary.ViewModels
{
    public class IlterisDictionaryVm : VmBase, INotifyPropertyChanged
    {

        private IEnumerable<IlterisDictionaryEntryVm> entries = [];
        public IEnumerable<IlterisDictionaryEntryVm> Entries
        {
            get => entries;
            set
            {
                entries = value;
                NotifyPropertyChanged();
            }
        }
    }
}
