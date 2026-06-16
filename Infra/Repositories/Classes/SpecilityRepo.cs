using Core;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Classes
{
    public class SpecilityRepo:GenericRepo<Specility>,ISpecility
    {
        HospitalContext hospitalContext;

        public SpecilityRepo(HospitalContext hospitalContext) : base(hospitalContext)
        {
            this.hospitalContext = hospitalContext;
        }

        public async Task<List<CheckBoxDto>> GetSpecilityCheckBoxes(long doctorid = 0)
        {
            if (doctorid == 0)
            {
                var res = from t in this.hospitalContext.Specilities
                          select new CheckBoxDto
                          {
                              IsSelected = false,
                              Text=t.SpecilityName,
                              Value=t.SpecilityID
                          };
                return await res.ToListAsync();
            }
            else
            {
                var docSpecility = this.hospitalContext.DoctorSpecialities.Where(p => p.DoctorID == doctorid);

                var res = from t in this.hospitalContext.Specilities
                          select new CheckBoxDto
                          {
                              IsSelected=docSpecility.Any(p=>p.SpecilityID==t.SpecilityID),
                              Text=t.SpecilityName,
                              Value=t.SpecilityID
                          };
                return await res.ToListAsync();
            }
        }
    }
}
