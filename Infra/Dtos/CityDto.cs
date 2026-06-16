using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Dtos
{
    //Used for cascading
   public class CityDto
    {
        public Int64 CityID {  get; set; }
        public string CityName { get; set; }
       
    }
}
