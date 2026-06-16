using Core;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Infra.Repositories.Classes
{
    public class MedicineRepo : GenericRepo<Medicine>, IMedicine
    {
        HospitalContext hospitalContext;
        public MedicineRepo(HospitalContext hospitalContext) : base(hospitalContext)
        {
            this.hospitalContext = hospitalContext;
        }

        public async Task<List<MedicineDto>> GetMedicine(long MID)
        {

            var res = from t in hospitalContext.Medicine
                      where (Int64)t.MedicineTypes == MID
                      select new MedicineDto
                      {
                        MedicineID = t.MedicineID,
                        MedicineName = t.MedicineName,
                      };
            return await res.ToListAsync();
        }

       
    }
}




