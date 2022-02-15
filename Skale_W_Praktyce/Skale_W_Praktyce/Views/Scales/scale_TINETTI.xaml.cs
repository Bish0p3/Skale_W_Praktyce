using Skale_W_Praktyce.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class scale_TINETTI : ContentPage
    {
        public scale_TINETTI()
        {
            InitializeComponent();
            BindingContext = new scale_TINETTI_VM();

        }

    }
}