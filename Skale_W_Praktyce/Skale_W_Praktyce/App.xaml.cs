using Skale_W_Praktyce.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

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

    }
}
