using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
  public class Area
    {
        public Int64 AreaID {  get; set; }
        public string AreaName { get; set; }
        public Int64 CityID {  get; set; }
        public virtual City City { get; set; }

        public virtual List<Doctor> Doctors { get; set; }

    }
}
