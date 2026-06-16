using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Dtos
{
   public class AppointmentInfo
    {
        public Int64 DoctorClinicSessionID {  get; set; }
        public string StartTime {  get; set; }
        public string EndTime { get; set; }
        public DateTime AppointmentDate {  get; set; }
    }
}
