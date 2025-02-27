using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace MAUIBlazorHost
{
    public static class MauiProgram
    {
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				});

			builder.Services.AddMauiBlazorWebView();
			builder.Services.AddBlazorWebViewDeveloperTools();

#if DEBUG
			builder.Logging.AddDebug();
			builder.Logging.SetMinimumLevel(LogLevel.Debug);
#endif

			return builder.Build();
		}
	}
}
