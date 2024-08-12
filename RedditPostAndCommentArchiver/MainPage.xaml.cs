using CommunityToolkit.Maui.Storage;
using RedditPostAndCommentArchiver.ViewModels;

namespace RedditPostAndCommentArchiver
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            PostArchiverVm subredditVm = new();
            BindingContext = subredditVm;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await CallFilePicker();
                LabelFullPath.Text = result?.FullPath;
            }
            catch (Exception ex)
            {
                //something happened
            }
        }

        private async Task<FileResult> CallFilePicker()
        {
            var pickertypes = new FilePickerFileType(
                            new Dictionary<DevicePlatform, IEnumerable<string>>
                            {
                    { DevicePlatform.WinUI, new[] { ".json" } },
                            });

            var pickOptions = new PickOptions()
            {
                FileTypes = pickertypes,
                PickerTitle = "Select a savefile to append your entry to it or select a folder to create a savefile in",
            };
            return await FilePicker.Default.PickAsync(pickOptions);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                var result = await CallFolderPicker();
                LabelFullPath.Text = result?.Folder?.Path;
            }
            catch (Exception ex)
            {
                //something happened
            }
        }

        private async Task<FolderPickerResult> CallFolderPicker()
        {
            return await FolderPicker.Default.PickAsync();
        }
    }
}
