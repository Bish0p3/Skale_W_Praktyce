﻿using Skale_W_Praktyce.Data;
using Skale_W_Praktyce.Models;
using Skale_W_Praktyce.Views;
using Skale_W_Praktyce.Views.Flyout;
using Skale_W_Praktyce.Views.Scales;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Skale_W_Praktyce.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {

        #region Constructor
        public ViewModel(INavigation navigation)
        {
            Navigation = navigation;

            Users = new ObservableCollection<User>() { };

            FirstRun();
            InitUsersAsync();



            #region Login Page Commands
            Login_SelectUser_Clicked = new Command(async () => await Login_SelectUser_Method());
            Login_AddUserButton_Clicked = new Command(async () => await Login_AddUserButton_Method());
            #endregion

            #region Main Page Commands
            BrowseScalesButton_Clicked = new Command(async () => await BrowseScalesButton_Method());
            CategoriesButton_Clicked = new Command(async () => await CategoriesButton_Method());
            FavoritesButton_Clicked = new Command(async () => await FavoritesButton_Method());
            HelpButton_Clicked = new Command(HelpButton_Method);
            LogoutButton_Clicked = new Command(async () => await LogoutButton_Method());
            #endregion

            #region Register Page Commands
            AddUser_AddUserButton_Clicked = new Command(async () => await AddUser_AddUserButton_Method());
            AddUser_MaleIcon_Clicked = new Command(async () => await AddUser_MaleIcon_Method());
            AddUser_FemaleIcon_Clicked = new Command(async () => await AddUser_FemaleIcon_Method());
            #endregion
        }
        #endregion

        #region Commands
        #region Login Page Commands
        public ICommand Login_SelectUser_Clicked { get; set; }
        public ICommand Login_AddUserButton_Clicked { get; set; }
        #endregion

        #region Main Page Commands
        public ICommand BrowseScalesButton_Clicked { get; set; }
        public ICommand CategoriesButton_Clicked { get; set; }
        public ICommand FavoritesButton_Clicked { get; set; }
        public ICommand HelpButton_Clicked { get; set; }
        public ICommand LogoutButton_Clicked { get; set; }

        #endregion

        #region Register Page Commands
        public ICommand AddUser_AddUserButton_Clicked { get; set; }
        public ICommand AddUser_MaleIcon_Clicked { get; set; }
        public ICommand AddUser_FemaleIcon_Clicked { get; set; }
        #endregion
        #endregion

        #region Fields
        private ObservableCollection<User> users;
        private string userNotificationText;
        private bool usernameNotificationVisibility;
        #region Register Page
        private string addUser_UsernameEntry;
        private string addUser_UserImage;
        #endregion

        #endregion

        #region Properties
        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                if (users != value)
                {
                    users = value;
                    OnPropertyChanged("Users");
                }
            }
        }

        // Navigation
        public INavigation Navigation { get; set; }

        //PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        #region Register Page properties
        public string AddUser_UsernameEntry
        {
            set
            {
                if (addUser_UsernameEntry != value)
                {
                    addUser_UsernameEntry = value;
                    OnPropertyChanged("AddUser_UsernameEntry");
                }
            }
            get
            {
                return addUser_UsernameEntry;
            }

        }
        public string UserNotificationText
        {
            get { return userNotificationText; }
            set { userNotificationText = value; OnPropertyChanged("UserNotificationText"); }
        }
        public bool UserNotificationVisibility
        {
            get { return usernameNotificationVisibility; }
            set { usernameNotificationVisibility = value; OnPropertyChanged("UserNotificationVisibility"); }
        }
        public string AddUser_UserImage
        {
            get { return addUser_UserImage; }
            set { addUser_UserImage = value; OnPropertyChanged("AddUser_UserImage"); }
        }
        #endregion

        #endregion

        #region Methods

        #region Login page
        public async Task Login_AddUserButton_Method()
        {
            await Navigation.PushAsync(new RegisterPage());
        }
        public async Task Login_SelectUser_Method()
        {
            Application.Current.MainPage = new NavigationPage(new MainPage_Flyout());
            await Navigation.PopAsync();
        }
        #endregion

        #region Main Page methods
        public async Task BrowseScalesButton_Method()
        {
            await Navigation.PushAsync(new ScalesListPage());
        }
        public async Task CategoriesButton_Method()
        {
            await Navigation.PushAsync(new ScalesCategories());
        }
        public async Task FavoritesButton_Method()
        {
            await Navigation.PushAsync(new FavoriteScalesPage());
        }
        public void HelpButton_Method()
        {
            //await Navigation.PushAsync(new PatientsListPage());
        }
        public async Task LogoutButton_Method()
        {
            Application.Current.MainPage = new NavigationPage(new LogInPage());
            await Navigation.PopAsync();
        }

        #endregion

        #region Register Page methods
        public async Task AddUser_AddUserButton_Method()
        {
            if (!string.IsNullOrWhiteSpace(AddUser_UsernameEntry) || !string.IsNullOrWhiteSpace(AddUser_UserImage))
            {
                await App.Database.SaveUserAsync(new User()
                {
                    Username = AddUser_UsernameEntry,
                    UserImage = AddUser_UserImage
                });
                await Navigation.PopAsync();
            }
            else
            {
                // POPUP
                await Application.Current.MainPage.DisplayAlert("Uwaga",
                    "Uzupełnij wszystkie pola, wybierz opcje i spróbuj ponownie.", "OK");
            }
        }
        #endregion
        private async Task AddedUserNotification(bool IsAdded)
        {
            if (IsAdded)
            {
                UserNotificationText = "Dodano Użytkownika";
                UserNotificationVisibility = true;
                await Task.Delay(2000);
                UserNotificationVisibility = false;
            }
            else
            {
                UserNotificationText = "Usunięto Użytkownika";
                UserNotificationVisibility = true;
                await Task.Delay(2000);
                UserNotificationVisibility = false;
            }
        }

        public async Task FirstRun()
        {
            if (Settings.FirstRun)
            {
                await Application.Current.MainPage.DisplayAlert("Info", "Witaj w aplikacji Skale medyczne, jeśli potrzebujesz pomocy w poruszaniu się po aplikacji kliknij w opcję 'POMOC'. Znajdziesz tam również informacje jak korzystać ze skal.", "OK");
                // Perform an action such as a "Pop-Up".
                Settings.FirstRun = false;
            }
        }

        private async Task InitUsersAsync()
        {
            List<User> usersDB = await App.Database.GetUsersAsync();
            foreach (User user in usersDB)
            {
                Users.Add(user);
            }
            if (usersDB.Count == 0)
            {
                //pozniej dodaj napis brak uzyt
            }
        }
        private async Task AddUser_MaleIcon_Method()
        {
            AddUser_UserImage = "userm";
        }
        private async Task AddUser_FemaleIcon_Method()
        {
            AddUser_UserImage = "userf";
        }
        #endregion

        #region Helpers
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion
    }
}
