using Blazor_MVVM_BaseComponents.Interfaces;
using Blazor_MVVM_BaseComponents.ViewModel;
using Blazor_UI_BaseComponents.Data;
using Microsoft.AspNetCore.Components;

namespace Blazor_UI_BaseComponents.Helper_Components
{
	public partial class MessagesPaginationBar
	{
		[Parameter]
		public VmBase? ViewModel
		{
			get;
			set
			{
				field = value;
				if (value is IPaginationBase asPaginationBase)
				{
					PaginatedVm = asPaginationBase;
				}
			}
		}


		public IPaginationBase? PaginatedVm { get; private set; }


		[Parameter] public int NavbarBottomMargin { get; set; }


		private void AssignNewPage(int newPage, AddOrRemove addOrRemove, PaginationSteps paginationSteps)
		{
			switch (addOrRemove)
			{
				case AddOrRemove.None: AssignPageNumber(newPage, paginationSteps); break;
				case AddOrRemove.Add: AddPageNumber(newPage, paginationSteps); break;
				case AddOrRemove.Remove: SubtractPageNumber(newPage, paginationSteps); break;
			}
		}

		private void AssignPageNumber(int newPageNumber, PaginationSteps steps)
		{
			if (steps != PaginationSteps.None)
				throw new ArgumentException("There isnt a classification for individual stepping");
			PaginatedVm?.CurrentPage = newPageNumber;
			Task.Run(() => PaginatedVm?.LoadNextPage(PaginatedVm.CurrentPage == 1 ? 0 : PaginatedVm.CurrentPage - 1));
		}

		private void AddPageNumber(int newPageNumber, PaginationSteps steps)
		{
			int futurePageNumber = (PaginatedVm?.CurrentPage + newPageNumber >= 0
				? PaginatedVm?.CurrentPage + newPageNumber
				: throw new ArgumentOutOfRangeException(nameof(newPageNumber))).GetValueOrDefault();
			if (futurePageNumber > PaginatedVm?.PageCount)
			{
				PaginatedVm?.CurrentPage = PaginatedVm.PageCount;
			}
			else
			{
				switch (steps)
				{
					case PaginationSteps.Singular: PaginatedVm?.CurrentPage += newPageNumber; break;
					case PaginationSteps.Chunk: PaginatedVm?.CurrentPage += PaginatedVm?.CurrentPage == 1 ? newPageNumber - 1 : newPageNumber; break;
					case PaginationSteps.All: PaginatedVm?.CurrentPage = PaginatedVm.PageCount; break;
				}
			}
			Task.Run(() => PaginatedVm?.LoadNextPage(PaginatedVm.CurrentPage - 1));
		}

		private void SubtractPageNumber(int newPageNumber, PaginationSteps steps)
		{
			if (PaginatedVm?.CurrentPage - newPageNumber <= 0)
			{
				PaginatedVm?.CurrentPage = 1;
				Task.Run(() => PaginatedVm?.LoadNextPage(0));
			}
			else
			{
				PaginatedVm?.CurrentPage -= newPageNumber;
				Task.Run(() => PaginatedVm?.LoadNextPage(PaginatedVm.CurrentPage - 1));
			}
		}
	}
}
