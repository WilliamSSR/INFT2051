using CycleWise.Pages;

namespace CycleWise;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(LogPage), typeof(LogPage));
        Routing.RegisterRoute(nameof(HistoryPage), typeof(HistoryPage));
    }
}
