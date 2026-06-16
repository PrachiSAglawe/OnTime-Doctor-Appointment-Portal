using Core;
using Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Interfaces
{
   public interface IDoctorClinicSession:IGeneric<DoctorClinicSession>
    {
        //string GetEndTime(Int64 DoctorClinicSessionID);
        Task<RepoResultDto> BookAppointmentPatient(BookAppointmentDto rec);
    }

}
