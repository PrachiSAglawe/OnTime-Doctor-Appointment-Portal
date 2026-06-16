using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
   public class User
    {
        [Key]
       
        public Int64 UserID {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string EmailID { get; set; }
        public string MobileNo {  get; set; }
        public string Address { get; set; }
        public string Password {  get; set; }
        public Int64 CountryID {  get; set; }
        public virtual Country Country { get; set; }

        public virtual List<Patient> Patients { get; set; }
        



    }
}
