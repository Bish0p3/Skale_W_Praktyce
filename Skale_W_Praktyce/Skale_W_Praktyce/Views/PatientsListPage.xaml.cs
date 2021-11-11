using Skale_W_Praktyce.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Skale_W_Praktyce
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientsListPage : ContentPage
    {
        public PatientsListPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ViewModel a = new ViewModel();
            a.LoadPatients();
        }
    }
}