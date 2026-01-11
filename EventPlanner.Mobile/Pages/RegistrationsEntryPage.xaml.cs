using EventPlanner.Mobile.Models;
using EventPlanner.Mobile.Services;

namespace EventPlanner.Mobile.Pages;

public partial class RegistrationsEntryPage : ContentPage
{
    private readonly ApiService _api = new();
    private List<Participant> _participants = new();
    private List<EventItem> _events = new();

    public RegistrationsEntryPage()
    {
        InitializeComponent();

        if (ApiService.IsAdmin)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Access denied", "Admins cannot register to events.", "OK");
                await Shell.Current.GoToAsync("//Events");
            });
            return;
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadData();
    }

    private async Task LoadData()
    {
        try
        {
            _participants = await _api.GetParticipantsAsync();
            ParticipantPicker.ItemsSource = _participants;
            ParticipantPicker.ItemDisplayBinding = new Binding("LastName");

            _events = await _api.GetEventItemsAsync();
            EventPicker.ItemsSource = _events;
            EventPicker.ItemDisplayBinding = new Binding("Name");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        if (ParticipantPicker.SelectedItem is not Participant p ||
            EventPicker.SelectedItem is not EventItem ev)
        {
            await DisplayAlert("Validation", "Select participant and event.", "OK");
            return;
        }

        var ok = await _api.CreateRegistrationAsync(new Registration
        {
            ParticipantId = p.ID,
            EventItemId = ev.ID
        });

        await DisplayAlert(ok ? "OK" : "Error", ok ? "Registered successfully!" : "Registration failed.", "OK");
    }
}
