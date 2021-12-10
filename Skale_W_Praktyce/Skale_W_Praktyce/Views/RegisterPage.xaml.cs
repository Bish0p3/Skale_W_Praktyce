using Skale_W_Praktyce.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new ViewModel(Navigation);
        }

        private void EmailEntry_Completed(object sender, EventArgs e)
        {
            LoginEntry.Focus();
        }
        private void LoginEntry_Completed(object sender, EventArgs e)
        {
            PasswordEntry.Focus();
        }
    }
}