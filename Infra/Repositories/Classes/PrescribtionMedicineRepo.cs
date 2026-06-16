using Core;
using Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Classes
{
    public class PrescriptionMedicineRepo : GenericRepo<PrescriptionMedicine>,IPrescribtionMedicine
    {
        HospitalContext hospitalContext;
        public PrescriptionMedicineRepo(HospitalContext hospitalContext) : base(hospitalContext) { }
    }
}
