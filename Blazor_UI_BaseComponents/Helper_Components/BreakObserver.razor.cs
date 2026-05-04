using Blazor_MVVM_BaseComponents.ViewModel;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace Blazor_UI_BaseComponents.Helper_Components
{
    public partial class BreakObserver
    {
        [Parameter] public IEnumerable<ComponentBase> Components { get; set; } = new List<ComponentBase>();
        [Parameter] public IEnumerable<VmBase> VMs { get; set; } = [];
        [Parameter] public Action Action { get; set; } = delegate { };

        public void Break() { Debugger.Break(); Action.Invoke(); }

        protected override Task OnInitializedAsync() { ShowBreakComponentIfDebug(); return base.OnInitializedAsync(); }
        protected override void OnInitialized() { ShowBreakComponentIfDebug(); base.OnInitialized(); }

        private bool _showBreakComponent;
        private void ShowBreakComponentIfDebug()
        {
#if DEBUG
            _showBreakComponent = true;
#endif
        }
    }
}
