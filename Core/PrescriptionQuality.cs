using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class PrescriptionQuality
    {
      public Int64 PrescriptionQualityID {  get; set; }
      public Int64 PrescriptionID {  get; set; }    
      public string NextVisit {  get; set; }
       public string Suggestion {  get; set; }

       public virtual Prescription Prescription { get; set; }
    }
}
