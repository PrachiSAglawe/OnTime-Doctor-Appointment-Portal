using Core;
using Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Interfaces
{
  public interface ICity:IGeneric<City>
    {
        Task<List<CityDto>> GetCityByStateID(Int64 id);

       
    }
}
