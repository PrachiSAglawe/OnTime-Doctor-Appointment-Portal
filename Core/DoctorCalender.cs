using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class DoctorCalender
    {
        [Key]
        public Int64 DoctorCalenderID {  get; set; }
        public Int64 DoctorClinicSessionID {  get; set; }
        public virtual DoctorClinicSession DoctorClinicSession { get; set; }
        public DateTime Fromdate {  get; set; }
        public DateTime Todate { get; set; }

        

    }
}
