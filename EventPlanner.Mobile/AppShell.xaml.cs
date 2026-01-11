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
}
