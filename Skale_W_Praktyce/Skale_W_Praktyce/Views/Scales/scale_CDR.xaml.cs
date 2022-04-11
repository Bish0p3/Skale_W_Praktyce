using Skale_W_Praktyce.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class scale_CDR : ContentPage
    {
        public scale_CDR()
        {
            InitializeComponent();
            BindingContext = new Scale_CDR_VM();
        }
    }
}