using Skale_W_Praktyce.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class scale_GLASGOW : ContentPage
    {
        public scale_GLASGOW()
        {
            InitializeComponent();
            BindingContext = new ScalesViewModel(Navigation);
            
        }

    }
}