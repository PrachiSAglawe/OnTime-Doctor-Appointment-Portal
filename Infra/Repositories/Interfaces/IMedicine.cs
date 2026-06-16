using Core;
using Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Infra.Repositories.Interfaces
{
    public interface IMedicine:IGeneric<Medicine>
    {
       
        Task<List<MedicineDto>> GetMedicine(Int64 MID);
    }
}
