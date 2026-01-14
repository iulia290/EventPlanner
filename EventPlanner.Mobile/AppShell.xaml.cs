using EventPlanner.Mobile.Pages;
using EventPlanner.Mobile.Services;

namespace EventPlanner.Mobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        OrganizersTab.IsVisible = ApiService.IsAdmin;

        ParticipantsTab.IsVisible = !ApiService.IsAdmin;
        RegisterTab.IsVisible = !ApiService.IsAdmin;
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        ApiService.Logout();
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }

}
