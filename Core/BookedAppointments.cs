using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class BookedAppointments
    {
        [Key]
        public Int64 BookedAppointmentsID {  get; set; }
        public DateTime AppointmentDate { get; set; }
        public string StartTime {  get; set; }
        public string EndTime { get; set; }
        public Int64 DoctorClinicSessionID {  get; set; }
        public virtual DoctorClinicSession DoctorClinicSession { get; set; }
        public Int64 PatientID {  get; set; }
        public virtual Patient Patient { get; set; }

        public bool IsPaid {  get; set; }

        public virtual List<BookedAppPayment> BookedAppPayments { get; set; }
        public virtual List<Prescription> Prescriptions { get; set; }
    }
}
