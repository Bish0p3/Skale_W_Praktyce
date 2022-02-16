using Skale_W_Praktyce.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class scale_NORTON : ContentPage
    {
        public scale_NORTON()
        {
            InitializeComponent();
            BindingContext = new scale_NORTON_VM();

        }

    }
}