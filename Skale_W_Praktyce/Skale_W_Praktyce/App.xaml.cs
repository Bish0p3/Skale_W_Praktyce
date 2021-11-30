using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Skale_W_Praktyce.Views;
using Skale_W_Praktyce.Views.Flyout;
using System.Threading.Tasks;

namespace Skale_W_Praktyce
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LogInPage());
        }
        public INavigation Navigation { get; set; }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public async Task LogInButton_Method()
        {
            await Navigation.PushModalAsync(new LogInPage());
        }
    }
}
