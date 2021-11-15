using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Skale_W_Praktyce.Models
{
    public class Model
    {
        
    }

    public class Patient : INotifyPropertyChanged
    {
        #region Fields

        private string firstName;
        private string lastName;

        #endregion

        #region Properties

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    RaisePropertyChanged("FirstName");
                    RaisePropertyChanged("FullName");
                }
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    RaisePropertyChanged("LastName");
                    RaisePropertyChanged("FullName");
                }
            }
        }

        public string FullName
        {
            get
            {
                return firstName + " " + lastName;
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
