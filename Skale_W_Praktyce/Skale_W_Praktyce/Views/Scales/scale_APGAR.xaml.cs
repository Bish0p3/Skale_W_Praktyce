using Skale_W_Praktyce.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scale_APGAR : ContentPage
    {
        public Scale_APGAR()
        {
            InitializeComponent();
            BindingContext = new Scale_APGAR_VM();

        }

    }
}