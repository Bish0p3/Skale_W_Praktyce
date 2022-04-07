using Skale_W_Praktyce.Models;
using Skale_W_Praktyce.ViewModels;
using Skale_W_Praktyce.Views.Scales;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
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

            BindingContext = new FlyoutPageFlyoutViewModel(Navigation);
            ListView = MenuItemsListView;

        }
    }
}