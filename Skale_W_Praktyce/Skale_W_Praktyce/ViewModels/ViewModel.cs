using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Skale_W_Praktyce.Models;
using Skale_W_Praktyce.Views;

namespace Skale_W_Praktyce.ViewModels
{
    public class ViewModel
    {

        public ObservableCollection<Patient> Patients
        {
            get;
            set;
        }
        public void LoadPatients()
        {

            ObservableCollection<Patient> patients = new ObservableCollection<Patient>();

            patients.Add(new Patient { FirstName = "Mark", LastName = "Allain" });
            patients.Add(new Patient { FirstName = "Allen", LastName = "Allain" });
            patients.Add(new Patient { FirstName = "Linda", LastName = "Hamerski" });

            Patients = patients;
        }
    }
}
