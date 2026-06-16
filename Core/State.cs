using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class State
    {
        [Key]
        public Int64 StateID {  get; set; }
        public string StateName { get; set; }
        public Int64 CountryID {  get; set; }
        public virtual Country Country { get; set; }
        public virtual List<City> Citys { get; set; }

    }
}
