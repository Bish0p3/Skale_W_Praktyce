using System.Collections.ObjectModel;
using Skale_W_Praktyce.Models;

namespace Skale_W_Praktyce.ViewModels
{
    class PatientsViewModel
    {
        public ObservableCollection<Patient> PatientsList { get; set; }
        public PatientsViewModel()
        {
            PatientsList = new ObservableCollection<Patient>
            {
                new Patient(){ FirstName = "John", LastName = "Krasinski"},
                new Patient(){ FirstName = "Ela", LastName = "Jablonska"},
            };
        }
    }
}
