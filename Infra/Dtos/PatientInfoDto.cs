using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Infra.Dtos
{
    public class PatientInfoDto
    {
        public string FullName { get; set; }
        public string Address {  get; set; }
        public string MobileNo {  get; set; }
        public GenderEnum Gender { get; set; }
        public string EmailID {  get; set; }
    }
}
