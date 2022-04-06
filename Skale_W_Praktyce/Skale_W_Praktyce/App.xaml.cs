using Skale_W_Praktyce.Views;
using Xamarin.Forms;
using Skale_W_Praktyce.Data;
using System.IO;
using System;

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

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

    }
}
