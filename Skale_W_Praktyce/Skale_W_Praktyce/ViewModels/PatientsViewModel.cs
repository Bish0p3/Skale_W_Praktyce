using Skale_W_Praktyce.Models;
using System.Collections.ObjectModel;

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
