using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class OPDSession
    {
        [Key]
        public Int64 OPDSessionID {  get; set; }
        public string SessionName {  get; set; }
        public Int64 ClinicID { get; set; }
        public virtual Clinic Clinic { get; set; }

        public virtual List<DoctorClinicSession> DoctorClinicSessions { get; set; }
    }
}
