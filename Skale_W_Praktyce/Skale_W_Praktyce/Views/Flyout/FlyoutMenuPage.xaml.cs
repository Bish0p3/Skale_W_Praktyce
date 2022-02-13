using Skale_W_Praktyce.Views.Scales;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Flyout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutMenuPage : ContentPage
    {
        public ListView ListView;

        public FlyoutMenuPage()
        {
            InitializeComponent();

            BindingContext = new FlyoutPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class FlyoutPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<FlyoutPageFlyoutMenuItem> MenuItems { get; set; }

            public FlyoutPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<FlyoutPageFlyoutMenuItem>(new[]
                {
                    new FlyoutPageFlyoutMenuItem { Id = 0, Title = "Strona Główna", TargetType = typeof(MainPage_Flyout)},
                    new FlyoutPageFlyoutMenuItem { Id = 1, Title = "Przeglądaj Skale", TargetType = typeof(ScalesListPage)},
                    new FlyoutPageFlyoutMenuItem { Id = 2, Title = "Kategorie", TargetType = typeof(ScalesCategories) },
                    new FlyoutPageFlyoutMenuItem { Id = 3, Title = "Ulubione", TargetType = typeof(AddPatientPage) },
                    new FlyoutPageFlyoutMenuItem { Id = 4, Title = "Pomoc" , TargetType = typeof(AddPatientPage)},
                    new FlyoutPageFlyoutMenuItem { Id = 5, Title = "Wyloguj", TargetType = typeof(AddPatientPage)},
                });
            }

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
}