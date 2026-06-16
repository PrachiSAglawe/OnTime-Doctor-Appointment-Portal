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
    public interface IPrescribtion:IGeneric<Prescription>
    {
        Task <List<BookedAppointments>>GetPriscribed(Int64 doctorid, Int64 sessionid);
        Task<Prescription> GetByID(Int64 PID);
        Task<RepoResultDto> GetPrescribedFormInfo(PrescribeInfoDto rec, Int64[] MedicineID, string[] Frequency, string[] Dosage, string[] Qty, UnitEnum[] Unit);
        Task<decimal> GetOPDFees(Int64 bookapootid);
        Task<Int64> GetPrescribtion();
        Task<PrescribeInfoDto> GetPresByID(Int64 BookAppId);
        Task<RepoResultDto> EditPresforMultiple(PrescribeInfoDto rec, Int64[]MedicineID,string[] Frequency, string[]Dosage, string[]Qty,UnitEnum[] Unit);
    }
}
