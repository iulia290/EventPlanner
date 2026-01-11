using EventPlanner.Mobile.Services;

namespace EventPlanner.Mobile.Pages;

public partial class LoginPage : ContentPage
{
    private readonly ApiService _api = new();

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text?.Trim() ?? "";
        var pass = PassEntry.Text ?? "";

        if (email == "" || pass == "")
        {
            await DisplayAlert("Validation", "Completează email și parolă.", "OK");
            return;
        }

        var ok = await _api.LoginAsync(email, pass);
        if (!ok)
        {
            await DisplayAlert("Error", "Login failed.", "OK");
            return;
        }

        Application.Current.MainPage = new AppShell();

    }
}
