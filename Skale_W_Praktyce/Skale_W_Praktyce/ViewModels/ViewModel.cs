using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Skale_W_Praktyce.Models;
using Skale_W_Praktyce.Views;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Skale_W_Praktyce.Views.Flyout;

namespace Skale_W_Praktyce.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {

        #region Constructor

        public ViewModel(INavigation navigation)
        {
            Navigation = navigation;

            TestCommand = new Command(PerformEditorTestMethod);

            #region Login Page Commands

            LogInButton_Clicked = new Command(async () => await LogInButton_Method());
            RegisterButton_Clicked = new Command(async () => await RegisterButton_Method());
            LoginEntry_Completed = new Command(LoginEntry_Method);

            #endregion

            #region Main Page Commands
            CreateAProfileButton_Clicked = new Command(async () => await CreateAProfileButton_Method());
            BrowseScalesButton_Clicked = new Command(async () => await BrowseScalesButton_Method());
            MainLoginButton_Clicked = new Command(async () => await MainLoginButton_Method());
            #endregion
        }

        #endregion

        #region Fields

        private string editorText;
        private string labelText;
        private bool isEnabledEditor = true;

        #endregion

        #region Commands

        public ICommand TestCommand { get; set; }

        #region Login Page Commands
        public ICommand LogInButton_Clicked { get; set; }
        public ICommand RegisterButton_Clicked { get; set; }
        public ICommand LoginEntry_Completed { get; set; }

        #endregion

        #region Main Page Commands
        public ICommand CreateAProfileButton_Clicked { get;  set; }
        public ICommand BrowseScalesButton_Clicked { get; set; }

        public ICommand MainLoginButton_Clicked { get; set; }
        #endregion


        #endregion

        #region Properties
        // Navigation
        public INavigation Navigation { get; set; } 

        //PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

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
        public string EditorText
        {
            set
            {
                if(editorText != value)
                {
                    editorText = value;
                    OnPropertyChanged("EditorText");
                    OnPropertyChanged("LabelText");
                }
            }
            get
            {
                return editorText;
            }
        }

        public string LabelText
        {
            set
            {
                if (labelText != value)
                {
                    labelText = value;
                    OnPropertyChanged("LabelText");
                }
            }
            get
            {
                return labelText;
            }
        }

        #endregion

        #region Methods

        #region Login page
        public async Task LogInButton_Method()
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
            await Navigation.PopAsync();
        }
        public async Task RegisterButton_Method()
        {
            await Navigation.PushAsync(new RegisterPage());
        }
        public void LoginEntry_Method()
        {
            
        }
        #endregion

        #region Main Page methods
        public async Task MainLoginButton_Method()
        {
            Application.Current.MainPage = new NavigationPage(new LogInPage());
            await Navigation.PopAsync();
        }

        public async Task CreateAProfileButton_Method()
        {
            await Navigation.PushAsync(new PatientsListPage());
        }

        public async Task BrowseScalesButton_Method()
        {
            await Navigation.PushAsync(new ScalesCategories());
        }

        #endregion

        public void PerformEditorTestMethod()
        {
            IsEnabledEditor = false;
            LabelText = EditorText;
            OnPropertyChanged(nameof(IsEnabledEditor));
            OnPropertyChanged(nameof(LabelText));
            OnPropertyChanged(nameof(EditorText));
        }

        #endregion

        #region Helpers
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion
    }
}
