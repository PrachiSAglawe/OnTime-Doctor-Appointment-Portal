using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Specility
    {
        [Key]
        public Int64 SpecilityID {  get; set; }
        public string SpecilityName { get; set; }

        public virtual List<DoctorSpeciality> DoctorSpecialities { get; set; }
    }
}
