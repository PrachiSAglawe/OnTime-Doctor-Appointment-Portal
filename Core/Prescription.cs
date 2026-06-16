using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Prescription
    {
        public Int64 PrescriptionID {  get; set; }
        public string PrescriptionDate { get; set; }
        public Int64 BookedAppointmentsID {  get; set; }
        public virtual BookedAppointments BookedAppointments { get; set; }
        
        public Int64 DoctorID {  get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual List<PrescriptionMedicine> PrescriptionMedicines { get; set; }

        public virtual List<PrescriptionQuality> PrescriptionQuality { get; set; }

        public Prescription()
        {
            this.PrescriptionMedicines = new List<PrescriptionMedicine>();
            this.PrescriptionQuality = new List<PrescriptionQuality>();
        }


    }
}
