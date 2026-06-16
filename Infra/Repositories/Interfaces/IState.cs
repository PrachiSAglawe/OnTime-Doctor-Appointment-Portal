using Core;
using Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Interfaces
{
    public interface IState:IGeneric<State>
    {
        Task<List<StateDto>> GetStatebyID(Int64 CountryID);
        //Task<IEnumerable<State>> GetAllCountry();
        //Task<State> GetByIDC(Int64 id);
        Task<List<StateDto>> GetStateByCountryID(Int64 id);

    }
}
