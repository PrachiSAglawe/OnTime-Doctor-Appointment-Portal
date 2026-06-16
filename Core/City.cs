using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
   public class City
    {
        [Key]
        public Int64 CityID {  get; set; }
        public string CityName {  get; set; }
        public Int64 StateID {  get; set; }
        public virtual State State { get; set; }

        public virtual List<Area> Area { get; set; }
        public virtual List<Clinic> Clinic { get; set; }
    }
}
