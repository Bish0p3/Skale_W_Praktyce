using Skale_W_Praktyce.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Skale_W_Praktyce.ViewModels
{
    class ScaleCategoriesViewModel : INotifyPropertyChanged
    {
        public ScaleCategoriesViewModel(string category)
        {
            ScalesListCategory = new ObservableCollection<Scale>();

            ScalesViewModel scalesViewModel = new ScalesViewModel(navigation);
            for (int i = 0; i < scalesViewModel.ScalesList.Count; i++)
            {
                if (scalesViewModel.ScalesList[i].ScaleTags.Contains(category))
                {
                    ScalesListCategory.Add(scalesViewModel.ScalesList[i]);
                }
            }

        }
        private ObservableCollection<Scale> scalesListCategory;
        private readonly INavigation navigation;

        public ObservableCollection<Scale> ScalesListCategory
        {
            get { return scalesListCategory; }
            set
            {
                if (scalesListCategory != value)
                {
                    scalesListCategory = value;
                    OnPropertyChanged("ScalesListCategory");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
