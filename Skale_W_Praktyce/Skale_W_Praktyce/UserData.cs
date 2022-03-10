using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Skale_W_Praktyce
{
    internal class UserData : INotifyPropertyChanged
    {
        public UserData()
        {
            Favorites = new List<string>();
        }

        private List<string> favorites;

        public List<string> Favorites
        {
            get { return favorites; }
            set
            {
                favorites = value;
                OnPropertyChanged("Favorites");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
