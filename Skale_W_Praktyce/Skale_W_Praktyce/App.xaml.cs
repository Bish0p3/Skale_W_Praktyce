using Skale_W_Praktyce.Views;
using Xamarin.Forms;
using Skale_W_Praktyce.Data;
using System.IO;
using System;

namespace Skale_W_Praktyce
{
    public partial class App : Application
    {
        static BookmarkDatabase database;

        // Create the database connection as a singleton.
        public static BookmarkDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new BookmarkDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Bookmarks.db3"));
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
