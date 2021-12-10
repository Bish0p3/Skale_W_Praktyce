using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views.Flyout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage_Flyout : FlyoutPage
    {
        public MainPage_Flyout()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FlyoutPageTestFlyoutMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            FlyoutPage.ListView.SelectedItem = null;
        }
    }
}