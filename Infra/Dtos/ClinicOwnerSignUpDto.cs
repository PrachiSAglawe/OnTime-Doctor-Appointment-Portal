using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Dtos
{
   public class ClinicOwnerSignUpDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        [EmailAddress]
        public string EmailID { get; set; }
        [Required]
        public Int64 ClinicID { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and Confirm Password are not same")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ClinicName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ContactPersonName { get; set; }
        [Required]
        public string LandLineNo {  get; set; }
        [Required]
        public Int64 CityID {  get; set; }

        [Required]
        public Int64 StateID {  get; set; }
        [Required]
        public Int64 CountryID {  get; set; }


    }
}
