using System;
using System.ComponentModel;

namespace Skale_W_Praktyce.Models
{
    internal class Scale : INotifyPropertyChanged
    {

        public Scale()
        {
            ScaleViewName = typeof(Scale);
        }
        #region Fields

        private string scaleName;
        private string scaleDesc;
        private string scaleTags;
        private bool isFavorite;
        #endregion

        #region Properties
        public string ScaleName
        {
            get
            {
                return scaleName;
            }
            set
            {
                if (scaleName != value)
                {
                    scaleName = value;
                    RaisePropertyChanged("ScaleName");
                }
            }
        }

        public string ScaleDesc
        {
            get { return scaleDesc; }
            set
            {
                if (scaleDesc != value)
                {
                    scaleDesc = value;
                    RaisePropertyChanged("ScaleDesc");
                }
            }
        }

        public string ScaleTags
        {
            get { return scaleTags; }
            set
            {
                if (scaleTags != value)
                {
                    scaleTags = value;
                    RaisePropertyChanged("ScaleTags");
                }
            }
        }

        public bool IsFavorite
        {
            get { return isFavorite; }
            set
            {
                if (isFavorite != value)
                {
                    isFavorite = value;
                    RaisePropertyChanged("IsFavorite");
                }
            }
        }

        public Type ScaleViewName { get; set; }


        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion
    }

}
