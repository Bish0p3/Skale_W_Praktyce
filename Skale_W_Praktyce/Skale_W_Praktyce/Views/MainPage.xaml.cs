using Skale_W_Praktyce.ViewModels;
using Xamarin.Forms;

namespace Skale_W_Praktyce.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ViewModel(Navigation);
        }

    }
}
