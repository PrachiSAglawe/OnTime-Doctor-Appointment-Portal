using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Core
{
    public class Medicine
    {
        public Int64 MedicineID {  get; set; }
        public string MedicineName { get; set; }
        public string GenericName {  get; set; }
        public MedicineTypes MedicineTypes { get; set; }
        public virtual List<PrescriptionMedicine> PrescriptionMedicines { get; set; }
    }
}
