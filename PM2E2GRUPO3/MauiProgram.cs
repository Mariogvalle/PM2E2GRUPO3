using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using CommunityToolkit.Maui;


namespace PM2E2GRUPO3
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                .UseMauiCommunityToolkitMediaElement()
                .UseMauiMaps()
                .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit().UseMauiCommunityToolkitMediaElement();
            return builder.Build();
        }
    }
}