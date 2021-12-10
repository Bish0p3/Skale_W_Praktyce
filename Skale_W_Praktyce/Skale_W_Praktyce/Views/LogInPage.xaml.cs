using Skale_W_Praktyce.ViewModels;
using System;
using System.Data.SqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skale_W_Praktyce.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            InitializeComponent();
            BindingContext = new ViewModel(Navigation);
        }
        private void LoginButton_Clicked(object sender, EventArgs e)
        {

        }
        private void LoginEntry_Completed(object sender, EventArgs e)
        {
            Password.Focus();
        }
        private void ConnectDB_Button(object sender, EventArgs e)
        {
            try
            {
                string srvrdbname = "skalewpraktyce_db";
                string srvrname = "192.168.1.104";
                string srvrusername = "admin";
                string srvrpassword = "admin";

                string sqlconn = $"Data Source={srvrname}; Initial Catalog ={srvrdbname}; User ID={srvrusername};Password={srvrpassword};";

                SqlConnection sqlConnection = new SqlConnection(sqlconn);

                sqlConnection.Open();

                connectionStatus_Label.Text = "OPEN";
            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}