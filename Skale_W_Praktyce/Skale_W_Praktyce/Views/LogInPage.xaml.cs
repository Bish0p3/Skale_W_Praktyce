using Skale_W_Praktyce.ViewModels;
using System;
using System.Data.SqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            InitializeComponent();
            BindingContext = new ViewModel(Navigation);
        }
        private void LoginEntry_Completed(object sender, EventArgs e)
        {
            Password.Focus();
        }
    }
}