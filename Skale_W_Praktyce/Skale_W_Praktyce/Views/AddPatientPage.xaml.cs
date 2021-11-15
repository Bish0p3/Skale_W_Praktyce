using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            editore.IsEnabled = false;
            labelTest.Text = editore.Text;
        }
    }
}