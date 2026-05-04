using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor_MVVM_BaseComponents.Interfaces
{
	public interface IPaginationBase
	{
		public const int PageRange = 10;

		public int RangeStart => CurrentPage < 10 ? 1 : CurrentPage - CurrentPage % 10 - 1;
		public int RangeEnd => RangeStart + PageRange > PageCount ? PageCount : RangeStart + PageRange + 1;



		public int PageCount { get; set; }
		public int CurrentPage { get; set; }




		public void LoadNextPage(int nextPageNumber);
	}
}
