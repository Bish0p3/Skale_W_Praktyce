using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Skale_W_Praktyce.Views;
using Skale_W_Praktyce.Views.Flyout;
using Skale_W_Praktyce.ViewModels;

namespace Skale_W_Praktyce.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ViewModel(Navigation);
        }

        //async void createAProfileButton_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new PatientsListPage());
        //}

        //async void browseScalesButton_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new ScalesCategories());
        //}
    }
}
