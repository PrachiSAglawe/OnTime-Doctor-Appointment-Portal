using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ClinicRating
    {
        [Key]
        public Int64 ClinicRatingID {  get; set; }
        public Int64 ClinicID {  get; set; }
        public virtual Clinic Clinic { get; set; }
        public int Rating {  get; set; }

    }
}
