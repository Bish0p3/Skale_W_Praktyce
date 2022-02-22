using Skale_W_Praktyce.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class scale_GDS : ContentPage
    {
        public scale_GDS()
        {
            InitializeComponent();
            BindingContext = new scale_GDS_VM();

        }

    }
}