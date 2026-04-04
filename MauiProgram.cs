using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using CycleWise.Services;
using CycleWise.Pages;

namespace CycleWise;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseLocalNotification()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Services
        builder.Services.AddSingleton<PeriodLogDatabase>();
        builder.Services.AddSingleton<ReminderService>();

        // Pages
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<LogPage>();
        builder.Services.AddTransient<HistoryPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}