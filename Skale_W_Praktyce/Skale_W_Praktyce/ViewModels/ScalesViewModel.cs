﻿using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using Skale_W_Praktyce.Views.Flyout;
using System.Collections.ObjectModel;
using Skale_W_Praktyce.Models;
using System.Runtime.CompilerServices;

namespace Skale_W_Praktyce.ViewModels
{
    class ScalesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Scale> ScalesList { get; set; }

        //public ScalesViewModel(INavigation navigation)
        //{
        //    Navigation = navigation;

        //    NeurologyTapCommand = new Command(async () => await NeurologyTapMethod());
        //    RefreshScalesCommand = new Command(RefreshScalesMethod);


        //    ScalesList = new ObservableCollection<Scale>
        //    {
        //        new Scale(){ ScaleName = "John", ScaleDesc = "Krasinski"},
        //        new Scale(){ ScaleName = "Ela", ScaleDesc = "Jablonska"},
        //    };
        //}
        public ScalesViewModel()
        {
            NeurologyTapCommand = new Command(async () => await NeurologyTapMethod());
            ScaleFavButtonCommand = new Command(ScaleFavButtonMethod);



           ScalesList = new ObservableCollection<Scale>
                {
                    new Scale(){ ScaleName = "OPSEC", ScaleDesc = "LOREM IPSUM essasito essasito essasito essasito"},
                    new Scale(){ ScaleName = "OPSEC", ScaleDesc = "LOREM IPSUM essasito essasito essasito essasito"},
                    new Scale(){ ScaleName = "TEST 2 ", ScaleDesc = "Jablonska Jablonska Jablonska Jablonska Jablonska"},
                };


        }

        #region Commands
        public INavigation Navigation { get; set; }

        public ICommand NeurologyTapCommand { get; }
        public ICommand RefreshScalesCommand { get; }
        public ICommand ScaleSelectTapCommand { get; }
        public ICommand ScaleFavButtonCommand { get; }
        #endregion

        #region Methods
        public async Task NeurologyTapMethod()
        {
            await Navigation.PushAsync(new ScalesListPage_Flyout());
        }

        public void ScaleFavButtonMethod()
        {

        }

        #endregion


        #region Helpers
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }


}