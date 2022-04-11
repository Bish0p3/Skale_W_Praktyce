using Skale_W_Praktyce.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scale_ALDRET : ContentPage
    {
        public Scale_ALDRET()
        {
            InitializeComponent();
            BindingContext = new Scale_ALDRET_VM();
        }
    }
}