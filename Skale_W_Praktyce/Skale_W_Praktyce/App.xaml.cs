using Skale_W_Praktyce.Data;
using Skale_W_Praktyce.Views;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Skale_W_Praktyce
{
    public partial class App : Application
    {
        static MainDatabase database;

        // Create the database connection as a singleton.
        public static MainDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new MainDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MainDatabase.db"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LogInPage());
        }
        public INavigation Navigation { get; set; }
        protected override void OnStart()
        {
            FirstRun();
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }

        public async Task FirstRun()
        {
            if (Settings.FirstRun)
            {
                await Application.Current.MainPage.DisplayAlert("Info", "Witaj w aplikacji Skale medyczne, aby korzystać z aplikacji, stwórz użytkownika, możesz to zrobić wybierając przycisk poniżej. Jeśli potrzebujesz pomocy w poruszaniu się po aplikacji kliknij w opcję 'POMOC' w menu głównym, dostępnym po wybraniu użytkownika.", "OK");
                // Perform an action such as a "Pop-Up".
                Settings.FirstRun = false;
            }
        }
    }
}
