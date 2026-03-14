using CycleWise.Services;

namespace CycleWise.Pages;

public partial class HistoryPage : ContentPage
{
    public HistoryPage()
    {
        InitializeComponent();
        LogsCollectionView.ItemsSource = PeriodLogStore.Instance.Logs;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CountLabel.Text = $"Total logs: {PeriodLogStore.Instance.Logs.Count}";
    }
}