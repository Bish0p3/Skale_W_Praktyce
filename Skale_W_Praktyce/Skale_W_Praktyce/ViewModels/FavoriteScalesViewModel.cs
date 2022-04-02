using Skale_W_Praktyce.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Skale_W_Praktyce.ViewModels
{
    internal class FavoriteScalesViewModel : INotifyPropertyChanged
    {
        public FavoriteScalesViewModel()
        {
            FavoriteScalesList = new ObservableCollection<Scale>();
            InitAsync();
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

        private async Task InitAsync()
        {
            ScalesViewModel scalesViewModel = new ScalesViewModel(navigation);

            Scale suggestion = (Scale)scalesViewModel.ScalesList.Where(scale => scale.ScaleName.Contains("glasgow"));
            FavoriteScalesList.Add(suggestion);
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
