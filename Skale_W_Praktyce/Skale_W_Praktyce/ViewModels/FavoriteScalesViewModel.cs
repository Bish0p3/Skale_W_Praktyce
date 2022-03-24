using Skale_W_Praktyce.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Skale_W_Praktyce.ViewModels
{
    internal class FavoriteScalesViewModel : INotifyPropertyChanged
    {
        public FavoriteScalesViewModel()
        {
            FavoriteScalesList = new ObservableCollection<Scale>();

            ScalesViewModel scalesViewModel = new ScalesViewModel(navigation);
        }


        private ObservableCollection<Scale> favoriteScalesList;
        private INavigation navigation;

        public ObservableCollection<Scale> FavoriteScalesList
        {
            get { return favoriteScalesList; }
            set
            {
                if (favoriteScalesList != value)
                {
                    favoriteScalesList = value;
                    OnPropertyChanged("FavoriteScalesList");
                }
            }
        }


        #region Helpers
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
