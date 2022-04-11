using Skale_W_Praktyce.Models;
using Skale_W_Praktyce.Views;
using Skale_W_Praktyce.Views.Flyout;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Skale_W_Praktyce.ViewModels
{
    public class FlyoutPageFlyoutViewModel : INotifyPropertyChanged
    {
        public FlyoutPageFlyoutViewModel(INavigation navigation)
        {
            Navigation = navigation;
            DeleteAccountButton = new Command(DeleteAccountButton_Method);
            DeleteAccountConfirm = new Command(async () => await DeleteAccountConfirm_Method());
            DeleteAccountBack = new Command(DeleteAccountBack_Method);

            MenuItems = new ObservableCollection<FlyoutPageFlyoutMenuItem>(new[]
            {
                    new FlyoutPageFlyoutMenuItem { Id = 0, Title = "Strona Główna", TargetType = typeof(MainPage_Flyout)},
                    new FlyoutPageFlyoutMenuItem { Id = 1, Title = "Przeglądaj Skale", TargetType = typeof(ScalesListPage)},
                    new FlyoutPageFlyoutMenuItem { Id = 2, Title = "Kategorie", TargetType = typeof(ScalesCategories) },
                    new FlyoutPageFlyoutMenuItem { Id = 3, Title = "Ulubione", TargetType = typeof(MainPage) },
                    new FlyoutPageFlyoutMenuItem { Id = 4, Title = "Pomoc" , TargetType = typeof(MainPage)},
                    new FlyoutPageFlyoutMenuItem { Id = 5, Title = "Zmień użytkownika", TargetType = typeof(LogInPage)},
                });
        }

        #region fields
        private bool forSureVisibility;
        #endregion

        #region properties
        public bool ForSureVisibility
        {
            get
            {
                return forSureVisibility;
            }
            set
            {
                forSureVisibility = value;
                OnPropertyChanged("ForSureVisibility");
            }
        }
        #endregion

        #region Commands
        public ICommand DeleteAccountButton;
        public ICommand DeleteAccountConfirm;
        public ICommand DeleteAccountBack;
        public INavigation Navigation;
        #endregion

        #region methods
        public void DeleteAccountButton_Method()
        {
            ForSureVisibility = true;
        }
        public async Task DeleteAccountConfirm_Method()
        {
            User x = await App.Database.GetUserAsyncID(Settings.CurrentUserID);
            await App.Database.DeleteUserAsync(x);
            ForSureVisibility = false;
            Application.Current.MainPage = new NavigationPage(new LogInPage());
            await Navigation.PopAsync();
        }
        public void DeleteAccountBack_Method()
        {
            ForSureVisibility = false;
        }
        #endregion

        public ObservableCollection<FlyoutPageFlyoutMenuItem> MenuItems { get; set; }


        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

}
