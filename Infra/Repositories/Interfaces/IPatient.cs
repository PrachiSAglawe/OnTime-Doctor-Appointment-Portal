using Core;
using Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Interfaces
{
    public interface IPatient:IGeneric<Patient>
    {
        Task<PatientInfoDto> GetPatientInfo(Int64 Patientid);
        Task<List<Patient>> GetAllPatient(DateTime Fromdate,DateTime Todate,Int64 DID);
        Task<List<PatientCollectionDto>>GetPatientAmount(DateTime Fromdate, DateTime Todate, Int64 DID);

    }
}
