using Skale_W_Praktyce.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scale_CDR : ContentPage
    {
        public Scale_CDR()
        {
            InitializeComponent();
            BindingContext = new Scale_CDR_VM();
        }
    }
}