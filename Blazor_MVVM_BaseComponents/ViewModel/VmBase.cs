using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Blazor_MVVM_BaseComponents.ViewModel
{
	public class VmBase<TData> : VmBase
	{
		protected TData Data { get; }
		protected VmBase(TData data) { Data = data; }
	}

	public class VmBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		protected ObservableCollection<T> CreateObservableCollection<T>(string name)
		{
			var collection = new ObservableCollection<T>();
			collection.CollectionChanged += (s, e) => NotifyPropertyChanged(name);
			return collection;
		}

		public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}