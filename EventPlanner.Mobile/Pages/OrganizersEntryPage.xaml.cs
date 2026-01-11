using EventPlanner.Mobile.Models;
using EventPlanner.Mobile.Services;

namespace EventPlanner.Mobile.Pages;

public partial class OrganizersEntryPage : ContentPage
{
    private readonly ApiService _api = new();

    public OrganizersEntryPage()
    {
        InitializeComponent();
        _ = LoadOrganizers();
    }

    private async Task LoadOrganizers()
    {
        try
        {
            var list = await _api.GetOrganizersAsync();
            OrganizersView.ItemsSource = list;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
    private async void OnRefreshClicked(object sender, EventArgs e)
    {
        await LoadOrganizers();
    }


    private async void OnLoadClicked(object sender, EventArgs e)
    {
        await LoadOrganizers();
    }


    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var name = NameEntry.Text?.Trim() ?? "";
        var email = EmailEntry.Text?.Trim() ?? "";

        if (name == "")
        {
            await DisplayAlert("Validation", "Name is required.", "OK");
            return;
        }

        if (email == "")
        {
            await DisplayAlert("Validation", "Email is required.", "OK");
            return;
        }

        var created = await _api.CreateOrganizerAsync(new Organizer
        {
            Name = name,
            Email = email
        });

        if (created == null)
        {
            await DisplayAlert("Error", "Save failed.", "OK");
            return;
        }

        await DisplayAlert("OK", $"Saved organizer with ID={created.ID}", "OK");

        NameEntry.Text = "";
        EmailEntry.Text = "";
        await LoadOrganizers();
    }
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is int id)
        {
            var confirm = await DisplayAlert("Confirm", "Delete this organizer?", "Yes", "No");
            if (!confirm) return;

            var ok = await _api.DeleteOrganizerAsync(id);
            if (!ok) await DisplayAlert("Error", "Delete failed. (Poate are events legate de el)", "OK");

            await LoadOrganizers();
        }
    }

}
