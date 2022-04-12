using Skale_W_Praktyce.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
        private readonly INavigation navigation;
        private bool noBookmarksVisibility = true;
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
        public bool NoBookmarksVisibility
        {
            get { return noBookmarksVisibility; }
            set { noBookmarksVisibility = value; OnPropertyChanged("NoBookmarksVisibility"); }
        }

        private async Task InitAsync()
        {
            ScalesViewModel scalesViewModel = new ScalesViewModel(navigation);
            List<Bookmark> bookmarks = await App.Database.GetBookmarksAsync();
            List<Bookmark> userBookmarks = new List<Bookmark>();
            foreach (Bookmark bookmark in bookmarks)
            {
                if (bookmark.UserID == Settings.CurrentUserID)
                {
                    userBookmarks.Add(bookmark);
                }
                if (userBookmarks.Count == 0)
                {
                    NoBookmarksVisibility = true;
                }
                else
                {
                    NoBookmarksVisibility = false;
                }
            }

            foreach (Bookmark bookmark in userBookmarks)
            {
                foreach (Scale scale in scalesViewModel.ScalesList)
                {
                    if (scale.ScaleName.Contains(bookmark.ScaleName))
                    {
                        FavoriteScalesList.Add(scale);
                    }
                }

            }


            //foreach (Scale scale in scalesViewModel.ScalesList)
            //{
            //    foreach (Bookmark bookmark in bookmarks)
            //    {
            //        if (scale.ScaleName.ToLower().Contains(bookmark.ScaleName.ToLower()))
            //        {
            //            FavoriteScalesList.Add(scale);
            //        }
            //    }

            //}
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
