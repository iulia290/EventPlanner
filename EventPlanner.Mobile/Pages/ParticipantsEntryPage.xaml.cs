using EventPlanner.Mobile.Models;
using EventPlanner.Mobile.Services;

namespace EventPlanner.Mobile.Pages;

public partial class ParticipantsEntryPage : ContentPage
{
    private readonly ApiService _api = new();

    public ParticipantsEntryPage()
    {
        InitializeComponent();

        if (ApiService.IsAdmin)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Access denied",
                    "Admins cannot manage participants.",
                    "OK");
                await Shell.Current.GoToAsync("//Events");
            });
            return;
        }

        _ = LoadParticipants();
    }

    private async Task LoadParticipants()
    {
        ParticipantsView.ItemsSource = await _api.GetParticipantsAsync();
    }

    private async void OnRefreshClicked(object sender, EventArgs e)
        => await LoadParticipants();

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var firstName = FirstNameEntry.Text?.Trim() ?? "";
        var lastName = LastNameEntry.Text?.Trim() ?? "";
        var email = EmailEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(firstName) ||
            string.IsNullOrWhiteSpace(lastName) ||
            string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Validation", "All fields are required.", "OK");
            return;
        }

        var created = await _api.CreateParticipantAsync(new Participant
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email
        });

        if (created == null)
        {
            await DisplayAlert("Error", "Save failed.", "OK");
            return;
        }

        FirstNameEntry.Text = "";
        LastNameEntry.Text = "";
        EmailEntry.Text = "";

        await LoadParticipants();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is int id)
        {
            await _api.DeleteParticipantAsync(id);
            await LoadParticipants();
        }
    }
}
