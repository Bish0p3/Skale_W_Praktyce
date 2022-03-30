using Skale_W_Praktyce.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class scale_ALDRET : ContentPage
    {
        public scale_ALDRET()
        {
            InitializeComponent();
            BindingContext = new Scale_ALDRET_VM();
        }
    }
}