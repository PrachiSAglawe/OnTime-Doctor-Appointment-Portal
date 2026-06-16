using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Dtos
{
   public class DoctorInfoDto
    {
        public Int64 DoctorID { get; set; }
        public string DoctorName {  get; set; }
        public string ClinicName {  get; set; }
        public string ClinicAddress {  get; set; }
        public string MobileNo {  get; set; }
        public string EmailID {  get; set; }
        public string PhotoPath {  get; set; }
        public string SpecilityName { get; set; }
    }
}
