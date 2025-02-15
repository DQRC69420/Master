using CommunityToolkit.Mvvm.Input;
using MAUIBlazorHost.Models;

namespace MAUIBlazorHost.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}