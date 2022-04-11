using Skale_W_Praktyce.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scale_GLASGOW : ContentPage
    {
        public Scale_GLASGOW()
        {
            InitializeComponent();
            BindingContext = new Scale_GLASGOW_VM();

        }

    }
}