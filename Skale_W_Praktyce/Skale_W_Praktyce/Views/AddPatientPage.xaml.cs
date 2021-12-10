using Skale_W_Praktyce.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPatientPage : ContentPage
    {
        public AddPatientPage()
        {
            InitializeComponent();
            BindingContext = new ViewModel(Navigation);
        }
    }
}