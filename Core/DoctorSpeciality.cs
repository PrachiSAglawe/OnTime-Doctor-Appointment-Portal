using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class DoctorSpeciality
    {
        [Key]
        public Int64 DoctorSpecialityID {  get; set; }
        public Int64 DoctorID {  get; set; }
        public virtual Doctor Doctor { get; set; }
        public Int64 SpecilityID {  get; set; }
        public virtual Specility Specility { get; set; }    

    }
}
