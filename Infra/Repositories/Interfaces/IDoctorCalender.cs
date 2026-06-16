using Core;
using Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Interfaces
{
   public interface IDoctorCalender:IGeneric<DoctorCalender>
    {
        Task<List<DoctorSessionDto>> GetSessionName();
        Task<List<string>> GetAvailableDates(Int64 doctorId);
       Task<List<GetSessionbyDIDDto>> GetSessionByDoctorID(Int64 doctorID);
        List<string> GetTimeSlots(Int64 sessionId);
        
    }
}
