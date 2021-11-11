using Skale_W_Praktyce.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Skale_W_Praktyce
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void createBrowseButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PatientsListPage());
        }

        async void browseScalesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScalesCategories());
        }
    }
}
