using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class DoctorRating
    {
        public Int64 DoctorRatingID {  get; set; }
        public Int64 DoctorID {  get; set; }
        public virtual Doctor Doctor { get; set; }

        public int Rating {  get; set; }

    }
}
