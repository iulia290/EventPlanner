using EventPlanner.Mobile.Models;
using EventPlanner.Mobile.Services;

namespace EventPlanner.Mobile.Pages;

public partial class EventItemAddPage : ContentPage
{
    private readonly ApiService _api = new();

    public EventItemAddPage()
    {
        InitializeComponent();

        if (!ApiService.IsAdmin)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Access denied",
                    "Only admins can create events.",
                    "OK");

                await Shell.Current.GoToAsync("//Events");
            });
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var name = NameEntry.Text?.Trim() ?? "";
        var location = LocationEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(location))
        {
            await DisplayAlert("Validation",
                "Name and Location are required.",
                "OK");
            return;
        }

        var created = await _api.CreateEventItemAsync(new EventItem
        {
            Name = name,
            Location = location
        });

        if (created == null)
        {
            await DisplayAlert("Error",
                "Failed to create event.",
                "OK");
            return;
        }

        await Shell.Current.GoToAsync("..");
    }
}
