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
    public partial class scale_CDR : ContentPage
    {
        public scale_CDR()
        {
            InitializeComponent();
            BindingContext = new Scale_CDR_VM();
        }
    }
}