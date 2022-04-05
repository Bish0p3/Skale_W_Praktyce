using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skale_W_Praktyce.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class scale_MMSE : ContentPage
    {
        public scale_MMSE()
        {
            InitializeComponent();
            BindingContext = new Scale_MMSE_VM();
        }
    }
}