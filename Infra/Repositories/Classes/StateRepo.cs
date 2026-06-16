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
    public class StateRepo : GenericRepo<State>, IState
    {
        HospitalContext hospitalContext;
        public StateRepo(HospitalContext hospitalContext):base(hospitalContext)
        {
            this.hospitalContext = hospitalContext;
        }

      

       

        public async Task<List<StateDto>> GetStateByCountryID(long id)
        {
            var res = from t in this.hospitalContext.States
                      where t.CountryID == id
                      select new StateDto
                      {
                          StateID = t.StateID,
                          StateName = t.StateName,

                      };
            return await res.ToListAsync();
        }
        //used for cascading
        public async Task<List<StateDto>> GetStatebyID(long CountryID)
        {
            var res = from t in this.hospitalContext.States
                      where t.CountryID == CountryID
                      select new StateDto
                      {
                          StateID = t.StateID,
                          StateName = t.StateName
                      };
            return await res.ToListAsync();
            
        }
    }
}
