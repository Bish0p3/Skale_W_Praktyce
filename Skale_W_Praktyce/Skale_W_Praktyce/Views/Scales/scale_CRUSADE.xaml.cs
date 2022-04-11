using Skale_W_Praktyce.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scale_CRUSADE : ContentPage
    {
        public Scale_CRUSADE()
        {
            InitializeComponent();
            BindingContext = new Scale_CRUSADE_VM();

        }

    }
}