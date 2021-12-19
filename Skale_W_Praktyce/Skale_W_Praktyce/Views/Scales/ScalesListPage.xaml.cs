using Skale_W_Praktyce.ViewModels;
using Skale_W_Praktyce.Views.Flyout;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Scales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScalesListPage : ContentPage
    {
        public ScalesListPage()
        {
            InitializeComponent();
        }

    async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await Navigation.PushAsync(new AddPatientPage());
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
