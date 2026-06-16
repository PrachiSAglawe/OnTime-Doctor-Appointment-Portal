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
    public class AreaRepo:GenericRepo<Area>,IArea
    {
        HospitalContext hospitalContext;
        public AreaRepo(HospitalContext hospitalContext) : base(hospitalContext)
        {
            this.hospitalContext = hospitalContext;
        }

        public async Task<List<AreaDto>> GetAreaByCityID(long cityID)
        {
            var res = from t in hospitalContext.Areas
                      where t.CityID == cityID
                      select new AreaDto
                      {
                          AreaID=t.AreaID,
                          AreaName=t.AreaName,
                      };
            return await res.ToListAsync();
        }
    }
}
