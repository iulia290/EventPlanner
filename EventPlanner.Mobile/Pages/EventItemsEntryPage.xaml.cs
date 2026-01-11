using EventPlanner.Mobile.Services;

namespace EventPlanner.Mobile.Pages;

public partial class EventItemsEntryPage : ContentPage
{
    private readonly ApiService _api = new();

    public EventItemsEntryPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadEvents();
    }

    private async Task LoadEvents()
    {
        try
        {
            EventsView.ItemsSource = await _api.GetEventItemsAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void OnLoadClicked(object sender, EventArgs e)
    {

        await LoadEvents();
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new EventItemAddPage());
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is int id)
        {
            var confirm = await DisplayAlert("Confirm", "Delete this event?", "Yes", "No");
            if (!confirm) return;

            var ok = await _api.DeleteEventItemAsync(id);
            if (!ok) await DisplayAlert("Error", "Delete failed.", "OK");

            await LoadEvents();
        }
    }


}
