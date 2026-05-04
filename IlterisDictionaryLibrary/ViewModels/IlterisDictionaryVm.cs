using IlterisDictionaryLibrary.DataProviders;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IlterisDictionaryLibrary.ViewModels
{
	public class IlterisDictionaryVm : VmBase, INotifyPropertyChanged
	{
		private readonly NavigationManager _manager;

		public IlterisDictionaryVm(NavigationManager manager)
		{
			_manager = manager;
			_dataProvider = new JsonDictionaryProvider(_manager);
		}

		private readonly JsonDictionaryProvider _dataProvider;

		private IEnumerable<IlterisDictionaryEntryVm> _entries = [];
		public IEnumerable<IlterisDictionaryEntryVm> Entries
		{
			get => _entries;
			set
			{
				_entries = value;
				NotifyPropertyChanged();
			}
		}
	}
}
