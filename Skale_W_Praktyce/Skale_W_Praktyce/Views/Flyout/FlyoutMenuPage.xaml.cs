using Skale_W_Praktyce.ViewModels;
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