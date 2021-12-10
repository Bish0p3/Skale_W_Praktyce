using Skale_W_Praktyce.Views;
using Skale_W_Praktyce.Views.Flyout;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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

            #region Register Page Commands
            RegisterSendButton_Clicked = new Command(async () => await RegisterSendButton_Method());

            #endregion
        }

        #endregion

        #region Fields

        private string editorText;
        private string labelText;
        private bool isEnabledEditor = true;

        #region Register Page
        private string register_EmailEntry;
        private string register_PasswordEntry;
        private string register_LoginEntry;
        #endregion

        #endregion

        #region Commands

        public ICommand TestCommand { get; set; }

        #region Login Page Commands
        public ICommand LogInButton_Clicked { get; set; }
        public ICommand RegisterButton_Clicked { get; set; }
        public ICommand LoginEntry_Completed { get; set; }

        #endregion

        #region Main Page Commands
        public ICommand CreateAProfileButton_Clicked { get; set; }
        public ICommand BrowseScalesButton_Clicked { get; set; }

        public ICommand MainLoginButton_Clicked { get; set; }
        #endregion

        #region Register Page Commands
        public ICommand RegisterSendButton_Clicked { get; set; }
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
                if (editorText != value)
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

        #region Register Page properties
        public string Register_EmailEntry
        {
            set
            {
                if (register_EmailEntry != value)
                {
                    register_EmailEntry = value;
                    OnPropertyChanged("Register_EmailEntry");
                }
            }
            get
            {
                return register_EmailEntry;
            }

        }
        public string Register_LoginEntry
        {
            set
            {
                if (register_LoginEntry != value)
                {
                    register_LoginEntry = value;
                    OnPropertyChanged("Register_EmailEntry");
                }
            }
            get
            {
                return register_LoginEntry;
            }

        }
        public string Register_PasswordEntry
        {
            set
            {
                if (register_PasswordEntry != value)
                {
                    register_PasswordEntry = value;
                    OnPropertyChanged("Register_EmailEntry");
                }
            }
            get
            {
                return register_PasswordEntry;
            }

        }
        #endregion

        #endregion

        #region Methods

        #region Login page
        public async Task LogInButton_Method()
        {

            Application.Current.MainPage = new NavigationPage(new MainPage_Flyout());
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

        #region Register Page methods
        public async Task RegisterSendButton_Method()
        {
            if (Register_PasswordEntry != "" && Register_LoginEntry != "" && Register_EmailEntry != "")
            {
                string srvrdbname = "skalewpraktyce_db";
                string srvrname = "153.19.163.39";
                string srvrusername = "admin";
                string srvrpassword = "admin";

                string connectionString = $"Data Source={srvrname}; Initial Catalog ={srvrdbname}; User ID={srvrusername};Password={srvrpassword};";
                string queryString = $"INSERT INTO [dbo].[usersTable](Email, Username, Password) VALUES ('{Register_EmailEntry}', '{Register_LoginEntry}', '{register_PasswordEntry}')";


                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, sqlConnection);
                    //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            //Console.WriteLine(String.Format("DONE"));// etc
                        }
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();
                        // POPUP
                        await Application.Current.MainPage.DisplayAlert("Rejestracja",
                            "Zostałeś zarejestrowany, na adres email została wysłana wiadomość z danymi logowania. \n Możesz się teraz zalogować.", "OK");

                    }
                }
            }
            else
            {
                // POPUP
                await Application.Current.MainPage.DisplayAlert("Uwaga",
                    "Uzupełnij wszystkie pola i spróbuj ponownie.", "OK");

            }
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
