using EventPlanner.Mobile.Models;
using EventPlanner.Mobile.Services;

namespace EventPlanner.Mobile.Pages;

public partial class ParticipantsEntryPage : ContentPage
{
    private readonly ApiService _api = new();
    private Participant? _selected;

    public ParticipantsEntryPage()
    {
        InitializeComponent();

        if (ApiService.IsAdmin)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Access denied", "Admins cannot manage participants.", "OK");
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

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _selected = e.CurrentSelection.FirstOrDefault() as Participant;
        if (_selected == null) return;

        FirstNameEntry.Text = _selected.FirstName;
        LastNameEntry.Text = _selected.LastName;
        EmailEntry.Text = _selected.Email;
    }

    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        if (_selected == null)
        {
            await DisplayAlert("Pick", "Select a participant first.", "OK");
            return;
        }

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

        _selected.FirstName = firstName;
        _selected.LastName = lastName;
        _selected.Email = email;

        var ok = await _api.UpdateParticipantAsync(_selected);
        await DisplayAlert(ok ? "OK" : "Error", ok ? "Updated!" : "Update failed.", "OK");

        await LoadParticipants();
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        _selected = null;
        FirstNameEntry.Text = "";
        LastNameEntry.Text = "";
        EmailEntry.Text = "";
        ParticipantsView.SelectedItem = null;
    }

}
