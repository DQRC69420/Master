using Blazor_MVVM_BaseComponents.ViewModel;
using Microsoft.AspNetCore.Components;

namespace AS4_GW_WebGUI.Helper_Components
{
    public partial class ComponentWithVmBase<TVm> : ComponentBase where TVm : VmBase
    {
        public ComponentWithVmBase() : base()
        {
            _propertyChangedHandler = (s, e) =>
            {
                InvokeAsync(StateHasChanged);
                Task.Yield();
            };
        }

        private TVm _viewModel = default!;
        [Inject]
        public TVm ViewModel
        {
            get => _viewModel;
            set => VmSetter(ref _viewModel, ref value);
        }

        protected void VmSetter(ref TVm oldVm, ref TVm newVm)
        {
            if (!Equals(oldVm, newVm))
            {
                if (oldVm != null)
                {
                    oldVm.PropertyChanged -= _propertyChangedHandler;
                }

                oldVm = newVm;
                oldVm.PropertyChanged += _propertyChangedHandler;
            }
        }

        private readonly System.ComponentModel.PropertyChangedEventHandler _propertyChangedHandler;
    }
}
