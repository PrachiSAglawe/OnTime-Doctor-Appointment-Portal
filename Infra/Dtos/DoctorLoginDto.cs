using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Dtos
{
    public class DoctorLoginDto
    {

        [Required(ErrorMessage = "Password Required..!")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Required(ErrorMessage = "Mobile No Required..!")]
        public string MobileNo { get; set; }

        public string SessionName{ get; set; }
        public Int64 OpdSessionID {  get; set; }
    }
}
