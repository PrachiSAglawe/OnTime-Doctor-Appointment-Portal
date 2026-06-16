using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Dtos
{
   public class CalendarEventDto
    {
       
                public string Title { get; set; } = "";
                public DateTime Start { get; set; }
                public DateTime End { get; set; }
                public bool AllDay { get; set; } = true;
                public string Color { get; set; } = "#ff4d4d";
           

    }
}
