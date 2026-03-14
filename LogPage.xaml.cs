using CycleWise.Models;
using CycleWise.Services;

namespace CycleWise.Pages;

public partial class LogPage : ContentPage
{
    public LogPage()
    {
        InitializeComponent();

        FlowPicker.ItemsSource = new List<string>
        {
            "Light",
            "Medium",
            "Heavy"
        };

        MoodPicker.ItemsSource = new List<string>
        {
            "Happy",
            "Calm",
            "Normal",
            "Tired",
            "Sad",
            "Stressed"
        };

        ResetForm();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (EndDatePicker.Date < StartDatePicker.Date)
        {
            await DisplayAlert("Invalid Date", "End date cannot be earlier than start date.", "OK");
            return;
        }

        if (FlowPicker.SelectedIndex == -1)
        {
            await DisplayAlert("Missing Input", "Please select a flow level.", "OK");
            return;
        }

        if (MoodPicker.SelectedIndex == -1)
        {
            await DisplayAlert("Missing Input", "Please select a mood.", "OK");
            return;
        }

        var newLog = new PeriodLog
        {
            StartDate = StartDatePicker.Date ?? DateTime.Today,
            EndDate = EndDatePicker.Date ?? DateTime.Today,
            FlowLevel = FlowPicker.SelectedItem?.ToString() ?? string.Empty,
            Mood = MoodPicker.SelectedItem?.ToString() ?? string.Empty,
            Notes = NotesEditor.Text?.Trim() ?? string.Empty
        };

        PeriodLogStore.Instance.AddLog(newLog);

        await DisplayAlert("Saved", "Your period log has been saved.", "OK");

        ResetForm();
        await Shell.Current.GoToAsync("//HistoryPage");
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        ResetForm();
    }

    private void ResetForm()
    {
        StartDatePicker.Date = DateTime.Today;
        EndDatePicker.Date = DateTime.Today;
        FlowPicker.SelectedIndex = -1;
        MoodPicker.SelectedIndex = -1;
        NotesEditor.Text = string.Empty;
    }
}