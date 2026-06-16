using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ClinicOwner
    {
        [Key]
        public Int64 ClinicOwnerID {  get; set; }
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
        public string Gender {  get; set; }
        public string EmailID {  get; set; }
        public string MobileNo {  get; set; }
        public string Password {  get; set; }
        public Int64 ClinicID {  get; set; }
        public virtual Clinic Clinic { get; set; }

    }
}
