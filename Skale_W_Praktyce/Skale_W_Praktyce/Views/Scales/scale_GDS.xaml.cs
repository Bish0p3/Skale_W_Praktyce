using Skale_W_Praktyce.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scale_GDS : ContentPage
    {
        public Scale_GDS()
        {
            InitializeComponent();
            BindingContext = new Scale_GDS_VM();

        }

    }
}