using Microsoft.Extensions.DependencyInjection;

namespace EventPlanner.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new EventPlanner.Mobile.Pages.LoginPage()));
        }
    }
}