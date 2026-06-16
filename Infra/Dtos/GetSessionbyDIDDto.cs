using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Dtos
{
    public class GetSessionbyDIDDto
    {
        public Int64 DoctorClinicSessionID { get; set; }
        public string SessionName {  get; set; }
        public int TimeInteval { get; set; }
    }
}
