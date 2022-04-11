using Skale_W_Praktyce.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scale_AUDIT : ContentPage
    {
        public Scale_AUDIT()
        {
            InitializeComponent();
            BindingContext = new Scale_AUDIT_VM();
        }
    }
}