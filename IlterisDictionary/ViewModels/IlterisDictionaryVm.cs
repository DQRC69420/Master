using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IlterisDictionary.ViewModels
{
    public class IlterisDictionaryVm : VmBase, INotifyPropertyChanged
    {
        public IEnumerable<IlterisDictionaryEntryVm> Entries { get; set; }


    }
}
