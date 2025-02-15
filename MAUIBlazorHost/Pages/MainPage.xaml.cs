using MAUIBlazorHost.Models;
using MAUIBlazorHost.PageModels;

namespace MAUIBlazorHost.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}