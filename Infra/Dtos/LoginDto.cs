using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage ="EmailID Required..!")]
        [EmailAddress(ErrorMessage ="Please Enter valid Email..!")]
        public string EmailID {  get; set; }

        [Required(ErrorMessage ="Password Required..!")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        public string SessionName { get; set; }

       
    }
}
