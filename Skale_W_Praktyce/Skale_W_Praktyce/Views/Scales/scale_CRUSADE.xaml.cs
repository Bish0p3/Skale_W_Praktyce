using Skale_W_Praktyce.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class scale_CRUSADE : ContentPage
    {
        public scale_CRUSADE()
        {
            InitializeComponent();
            BindingContext = new scale_CRUSADE_VM();

        }

    }
}