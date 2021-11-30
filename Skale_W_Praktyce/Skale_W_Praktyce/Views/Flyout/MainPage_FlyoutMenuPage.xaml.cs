using Skale_W_Praktyce.Views.Flyout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
                    new FlyoutPageTestFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new FlyoutPageTestFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new FlyoutPageTestFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new FlyoutPageTestFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new FlyoutPageTestFlyoutMenuItem { Id = 4, Title = "Dodaj pacjenta 5" , TargetType = typeof(AddPatientPage)},
                    new FlyoutPageTestFlyoutMenuItem { Id = 5, Title = "es", TargetType = typeof(MainPage)},
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