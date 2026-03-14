using System.Linq;
using CycleWise.Services;

namespace CycleWise.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshDashboard();
    }

    private void RefreshDashboard()
    {
        var store = PeriodLogStore.Instance;
        var latestLog = store.GetLatestLog();

        if (latestLog == null)
        {
            LatestLogLabel.Text = "No records yet.";
            PredictionLabel.Text = "Add at least one record to see a basic prediction.";
            return;
        }

        LatestLogLabel.Text = $"{latestLog.DateRangeText}\n{latestLog.SummaryText}";

        int cycleLength = EstimateCycleLength();
        DateTime predictedNextStart = latestLog.StartDate.AddDays(cycleLength);

        PredictionLabel.Text =
            $"Estimated cycle length: {cycleLength} days\nPredicted next period: {predictedNextStart:dd MMM yyyy}";
    }

    private int EstimateCycleLength()
    {
        var logs = PeriodLogStore.Instance.Logs
            .OrderBy(x => x.StartDate)
            .ToList();

        if (logs.Count < 2)
        {
            return 28;
        }

        var gaps = new List<int>();

        for (int i = 1; i < logs.Count; i++)
        {
            int diff = (logs[i].StartDate - logs[i - 1].StartDate).Days;
            if (diff > 0)
            {
                gaps.Add(diff);
            }
        }

        if (gaps.Count == 0)
        {
            return 28;
        }

        return (int)Math.Round(gaps.Average());
    }

    private async void OnGoToLogClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//LogPage");
    }

    private async void OnGoToHistoryClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HistoryPage");
    }
}