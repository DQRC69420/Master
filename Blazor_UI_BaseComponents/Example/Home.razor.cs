using Blazor_UI_BaseComponents.Helper_Components.Bases;
using Blazor_UI_BaseComponents.Helper_Components;
using Blazor_MVVM_BaseComponents.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Web;

namespace Blazor_UI_BaseComponents.Example
{
    public partial class Home : ComponentWithVmBase<HomeVm>
    {
        public Home() : base() { }

        private void ProcessConfigs(UserConfigurations configs)
        {
            CascadingColor = configs?.CascadingColor ?? CascadingColor;
            if (configs != null && configs?.Widths.Count != 0)
                Widths = configs.Widths;
        }

        public int HorizontalScrollbarHeight { get; set; }
        public Dictionary<Guid, int> Widths { get; set; } = [];

        private HashSet<long> _expandedRows = [];
        private HashSet<long> _expandedNrrRows = [];
        private const int NrrPreviewLength = 150;

        private async Task ToggleRow(long id)
        {
            var expanding = _expandedRows.Add(id);
            if (!expanding)
            {
                _expandedRows.Remove(id);
                _expandedNrrRows.Remove(id);
            }
            StateHasChanged();
            await SetRowHeight(id, expanding);
        }

        private void ToggleNrr(long id)
        {
            if (!_expandedNrrRows.Add(id))
                _expandedNrrRows.Remove(id);
            StateHasChanged();
        }

        private async Task SetLayoutConfigurations()
        {
            try
            {
                var configs = new UserConfigurations()
                {
                    Widths = await GetColumnWidths(),
                    Headers = ViewModel.Headers,
                    ExcludedHeaders = ViewModel.ExcludedHeaders,
                    CascadingColor = CascadingColor,
                    RowsPerPage = ViewModel.SinglePageCount,
                    ColumnIdToSortAfter = ViewModel.ColumnIdToSortAfter,
                };
                await SaveLayoutData(JsonSerializer.Serialize(configs));
            }
            catch (Exception ex)
            {
                Debugger.Break();
                await Alert(ex.Message);
            }
        }

        private async Task ResetPageLayout()
        {
            ViewModel.ColumnIdToSortAfter = null;
            await ViewModel.AssignNewSinglePageCount(10);
            ViewModel.ClearExcluded();
            ViewModel.OrderHeaders();
            await Task.Yield();
            await ResetColumnWidths();
            CascadingColor = "#FFFFFF";
            StateHasChanged();
        }

        private async Task<UserConfigurations> GetLayoutConfiguration(string storageKey)
        {
            var loaded = await JS.InvokeAsync<string?>("layoutStore.loadLocal", storageKey) ?? string.Empty;
            if (string.IsNullOrWhiteSpace(loaded))
                return new UserConfigurations();
            return JsonSerializer.Deserialize<UserConfigurations>(loaded) ?? new UserConfigurations();
        }

        private async Task ClearLocalStorage()
            => await JS.InvokeVoidAsync("localStorage.clear");

        private void IncludeColumn(Guid id) => ViewModel.RemoveExcluded(id);

        private static List<EdifactMessageInfo> DeserializeEdifactMessages(string? json)
        {
            if (string.IsNullOrWhiteSpace(json)) return [];
            try { return JsonSerializer.Deserialize<List<EdifactMessageInfo>>(json) ?? []; }
            catch { return []; }
        }

        private async Task OnSortChanged()
            => await ViewModel.LoadNextPage(0);

        private void AssignColumnId(ChangeEventArgs e)
            => ViewModel.ColumnIdToSortAfter = Guid.TryParse(e?.Value.ToString(), out var result) ? result : Guid.Empty;

        private async Task ChangeSinglePageCount(ChangeEventArgs e)
        {
            if (int.TryParse(e?.Value?.ToString() ?? "", out var newValue) && newValue > 0)
                await ViewModel.AssignNewSinglePageCount(newValue);
        }

        #region drag events
        private Guid _draggedColumnId = Guid.Empty;
        private Guid _enteredColumnId = Guid.Empty;

        private void DropIntoExclusionZone(DragEventArgs dragEventArgs) => ViewModel.AddExcluded(_draggedColumnId);
        private void HeaderDragStartEvent(DragEventArgs dragEventArgs, Guid id) => _draggedColumnId = id;
        private void HeaderDragOverEvent(DragEventArgs dragOverArgs, Guid id) => _enteredColumnId = _draggedColumnId != id ? id : Guid.Empty;

        private void HeaderDropEvent(DragEventArgs dropArgs)
        {
            if (_enteredColumnId == _draggedColumnId || _enteredColumnId == Guid.Empty || _draggedColumnId == Guid.Empty)
                return;
            ViewModel.TransferColumn(_draggedColumnId, _enteredColumnId);
        }
        #endregion

        #region colors
        private string _cascadingColor = string.Empty;
        public string CascadingColor
        {
            get => _cascadingColor;
            private set
            {
                if (!Equals(_cascadingColor, value))
                {
                    _cascadingColor = value;
                    _backgroundColor = value;
                    if (value.StartsWith("#"))
                    {
                        _foregroundElementsColor = AdjustColorBrightness(value, 0.8);
                        _sidebarColor = AdjustColorBrightness(value, 1);
                    }
                    StateHasChanged();
                }
            }
        }

        public async Task AssignCascadingColor(string parameter)
        {
            if (!parameter.StartsWith("#"))
            {
                CascadingColor = parameter;
                await Task.Yield();
                CascadingColor = await GetHexFromColorName();
            }
            else
            {
                CascadingColor = parameter;
            }
        }

        private string _backgroundColor = "skyblue";
        public string BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                if (!Equals(_backgroundColor, value) && string.IsNullOrWhiteSpace(CascadingColor))
                {
                    _backgroundColor = value;
                    StateHasChanged();
                }
            }
        }

        private string _foregroundElementsColor = "lightsteelblue";
        public string ForegroundElementsColor
        {
            get => _foregroundElementsColor;
            set
            {
                if (!Equals(_foregroundElementsColor, value) && string.IsNullOrWhiteSpace(CascadingColor))
                {
                    _foregroundElementsColor = value;
                    StateHasChanged();
                }
            }
        }

        private string _sidebarColor = "";
        public string SidebarColor
        {
            get => _sidebarColor;
            set
            {
                if (!Equals(_sidebarColor, value))
                {
                    _sidebarColor = value;
                    StateHasChanged();
                }
            }
        }

        private string AdjustColorBrightness(string hex, double factor)
        {
            hex = new string([.. hex.Skip(1)]);
            int r = Convert.ToInt32(hex.Substring(0, 2), 16);
            int g = Convert.ToInt32(hex.Substring(2, 2), 16);
            int b = Convert.ToInt32(hex.Substring(4, 2), 16);
            r = (int)(r + (255 - r) * factor);
            g = (int)(g + (255 - g) * factor);
            b = (int)(b + (255 - b) * factor);
            return $"#{r:X2}{g:X2}{b:X2}";
        }
        #endregion

        #region collapse-logic
        private bool _isFilterVisible;

        public void ToggleFilterTableVisibility()
        {
            _isFilterVisible = !_isFilterVisible;
            ViewModel.Filter?.NotifyPropertyChanged();
            StateHasChanged();
        }

        private string GetFilterTableClass() => _isFilterVisible ? "collapse show" : "collapse";
        #endregion

        #region lifecycle

        private long? _firstItemId;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
#if DEBUG
                //Use this to reinstantiate the local storage data in case "UserConfigurations" changes in structure
                //await ClearLocalStorage();
#endif
                if (!_isInitialized)
                {
                    await Init();
                    _isInitialized = true;
                    StateHasChanged();
                }
            }
            else
            {
                var currentFirstId = ViewModel?.Items?.FirstOrDefault()?.Id;
                if (currentFirstId != _firstItemId)
                {
                    _firstItemId = currentFirstId;
                    _expandedRows.Clear();
                    _expandedNrrRows.Clear();
                    await ResetAllRowHeights();
                }
            }
            await base.OnAfterRenderAsync(firstRender);
        }


        private bool _isInitialized = false;
        private async Task Init()
        {
            try
            {
                var configs = await GetLayoutConfiguration(nameof(UserConfigurations));
                await ViewModel.Init(configs);
                ProcessConfigs(configs ?? new UserConfigurations());
            }
            catch (Exception ex)
            {
                await Alert(ex.Message);
                throw;
            }
            await InitializeResizableColumns();
            await UpdateTableMargin();
            HorizontalScrollbarHeight = await GetHScrollbarHeight();
            //StateHasChanged();
        }


        protected override Task OnInitializedAsync() => base.OnInitializedAsync();
        #endregion
    }
}
