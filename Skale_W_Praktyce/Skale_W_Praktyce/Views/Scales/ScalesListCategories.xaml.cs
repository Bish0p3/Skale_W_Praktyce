using Skale_W_Praktyce.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScalesListCategories : ContentPage
    {
        public ScalesListCategories()
        {
            InitializeComponent();
            BindingContext = new ScalesViewModel(Navigation);
        }
    }
}