using Blazor_UI_BaseComponents.Data;
using Microsoft.AspNetCore.Components;

namespace Blazor_UI_BaseComponents.Helper_Components
{
	public partial class Separator
	{
		[Parameter] public int Thickness { get; set; } = 1;
		[Parameter] public string ColorName { get; set; } = "gray";
		[Parameter] public GapDirection Align { get; set; }
		[Parameter] public int Gap { get; set; } = 10;

		public string AssignMargin() => Align switch
		{
			GapDirection.None => $"{Gap}px",
			GapDirection.Horizontal => $"{Gap} 0px",
			GapDirection.Vertical => $"0 {Gap}px",
			_ => throw new NotImplementedException($"Implement 'Left, Right, Up, Down' directions")
		};
	}
}
