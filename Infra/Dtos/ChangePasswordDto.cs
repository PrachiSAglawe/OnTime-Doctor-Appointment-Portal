using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Dtos
{
   public class ChangePasswordDto
    {
        [Required(ErrorMessage ="Old Password Required..!")]
        [DataType(DataType.Password)]
        public string OldPassword {  get; set; }

        [Required(ErrorMessage ="New Password Required..!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required (ErrorMessage ="ReTypePassword Required..!")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Password and Retype Password are not same..!")]
        public string RetypePassword {  get; set; }
    }
}
