using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Skale_W_Praktyce.Models;
using Skale_W_Praktyce.Views;
using System.Windows.Input;

namespace Skale_W_Praktyce.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {

        #region Constructor

        public ViewModel()
        {


        }

        #endregion

        #region Fields

        private string name;
        private bool isEnabledEditor;

        private Command editorTestMethod;
        #endregion

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Patient> Patients
        {
            get;
            set;
        }

        public ICommand EditorTestMethod
        {
            get
            {
                if (editorTestMethod == null)
                {
                    editorTestMethod = new Command(PerformEditorTestMethod);
                }

                return editorTestMethod;
            }
        }
        public bool IsEnabledEditor
        {
            set
            {
                isEnabledEditor = value;
                OnPropertyChanged("IsEnabledEditor");
            }
            get
            {
                return isEnabledEditor;
            }
        }
        public string Name
        {
            set
            {
                if(name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
            get
            {
                return name;
            }
        }
        #endregion

        #region Methods
        public void PerformEditorTestMethod()
        {
            IsEnabledEditor = false;
        }
        public void LoadPatients()
        {

            ObservableCollection<Patient> patients = new ObservableCollection<Patient>();

            patients.Add(new Patient { FirstName = "Mark", LastName = "Allain" });
            patients.Add(new Patient { FirstName = "Allen", LastName = "Allain" });
            patients.Add(new Patient { FirstName = "Linda", LastName = "Hamerski" });

            Patients = patients;
        }

        #endregion

        #region Helpers
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public class CommandHandler : ICommand
        {
            private Action _action;
            private bool _canExecute;
            public CommandHandler(Action action, bool canExecute)
            {
                _action = action;
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                _action();
            }
        }
        #endregion
    }
}
