using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Skale_W_Praktyce.Models
{
    internal class Scale : INotifyPropertyChanged
    {
        #region Fields

        private string scaleName;
        private string scaleDesc;

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
