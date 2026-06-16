using Core;
using Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Interfaces
{
    public interface IArea:IGeneric<Area>
    {
        Task<List<AreaDto>> GetAreaByCityID(Int64 cityID);


    }
}
