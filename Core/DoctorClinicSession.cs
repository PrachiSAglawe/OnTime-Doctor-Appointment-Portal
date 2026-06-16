using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
   public class DoctorClinicSession
    {
        [Key]
        public Int64 DoctorClinicSessionID {  get; set; }

        //[ForeignKey("Doctor")]
        public Int64 DoctorID {  get; set; }
        public virtual Doctor Doctor { get; set; }

        //[ForeignKey("Clinic")]
        public Int64 ClinicID {  get; set; }
        public virtual Clinic Clinic { get; set; }

        //[ForeignKey("OPDSession")]
        public Int64 OPDSessionID {  get; set; }
        public virtual OPDSession OPDSession { get; set; }
        public string StartTime {  get; set; }
        public string EndTime { get; set; }
        public int TimeInterval {  get; set; }

        public virtual List<BookedAppointments> BookedAppointments { get; set; }
        public virtual List<DoctorCalender> DoctorCalenders { get; set; }
       
        
    }
}
