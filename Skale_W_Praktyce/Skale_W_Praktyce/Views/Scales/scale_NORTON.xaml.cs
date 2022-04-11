using Skale_W_Praktyce.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scale_NORTON : ContentPage
    {
        public Scale_NORTON()
        {
            InitializeComponent();
            BindingContext = new Scale_NORTON_VM();

        }

    }
}