using Skale_W_Praktyce.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScalesCategories : ContentPage
    {
        public ScalesCategories()
        {
            InitializeComponent();
            BindingContext = new ScalesViewModel(Navigation);
        }
    }
}