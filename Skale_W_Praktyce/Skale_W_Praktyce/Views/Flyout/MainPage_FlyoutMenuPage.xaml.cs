using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Skale_W_Praktyce.ViewModels;

namespace Skale_W_Praktyce.Views.Flyout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage_FlyoutMenuPage : ContentPage
    {
        public ListView ListView;

        public MainPage_FlyoutMenuPage()
        {
            InitializeComponent();

            BindingContext = new FlyoutPageTestFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class FlyoutPageTestFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<FlyoutPageTestFlyoutMenuItem> MenuItems { get; set; }

            public FlyoutPageTestFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<FlyoutPageTestFlyoutMenuItem>(new[]
                {
                    new FlyoutPageTestFlyoutMenuItem { Id = 0, Title = "Strona Główna", TargetType = typeof(MainPage_Flyout)},
                    new FlyoutPageTestFlyoutMenuItem { Id = 1, Title = "Przeglądaj Skale", TargetType = typeof(ScalesCategories)},
                    new FlyoutPageTestFlyoutMenuItem { Id = 2, Title = "Kategorie", TargetType = typeof(ScalesCategories) },
                    new FlyoutPageTestFlyoutMenuItem { Id = 3, Title = "Pacjenci", TargetType = typeof(AddPatientPage) },
                    new FlyoutPageTestFlyoutMenuItem { Id = 4, Title = "Źródła" , TargetType = typeof(AddPatientPage)},
                    new FlyoutPageTestFlyoutMenuItem { Id = 5, Title = "Wyloguj", TargetType = typeof(AddPatientPage)},
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