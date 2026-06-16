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
 public class CityRepo:GenericRepo<City>,ICity
    {
        HospitalContext hospitalContext;
        public CityRepo(HospitalContext hospitalContext) : base(hospitalContext)
        {
            this.hospitalContext = hospitalContext;
        }

        public async Task<List<CityDto>> GetCityByStateID(long id)
        {
            var res = from t in this.hospitalContext.Citys
                      where t.StateID == id
                      select new CityDto
                      {
                          CityID = t.CityID,
                          CityName = t.CityName,
                      };
            return await res.ToListAsync();
        }
    }
}
