using Skale_W_Praktyce.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scale_MMSE : ContentPage
    {
        public Scale_MMSE()
        {
            InitializeComponent();
            BindingContext = new Scale_MMSE_VM();
        }
    }
}