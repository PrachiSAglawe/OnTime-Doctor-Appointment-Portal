using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Core
{
    public class PrescriptionMedicine
    {
        public Int64 PrescriptionMedicineID {  get; set; }
        public string Dosage {  get; set; }
        public string Frequency {  get; set; }
        public string Qty {  get; set; }
        public UnitEnum Unit { get; set; }
        public Int64 MedicineID { get; set; }
        public virtual Medicine Medicine { get; set; }
        public Int64 PrescriptionID {  get; set; }
        public virtual Prescription Prescription { get; set; }

    }
}
