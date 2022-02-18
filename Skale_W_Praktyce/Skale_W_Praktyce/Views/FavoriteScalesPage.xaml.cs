﻿using Skale_W_Praktyce.Models;
using Skale_W_Praktyce.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoriteScalesPage : ContentPage
    {
        public FavoriteScalesPage()
        {
            InitializeComponent();
            BindingContext = new ScalesViewModel(Navigation);
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Scale;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.ScaleViewName);
            page.Title = item.ScaleName;
            await Navigation.PushAsync(page);
            ((ListView)sender).SelectedItem = null;
        }
    }
}
