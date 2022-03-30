using Skale_W_Praktyce.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class scale_APGAR : ContentPage
    {
        public scale_APGAR()
        {
            InitializeComponent();
            BindingContext = new Scale_APGAR_VM();

        }

    }
}