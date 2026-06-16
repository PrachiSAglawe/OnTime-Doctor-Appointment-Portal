using Core;
using Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Interfaces
{
    
    public interface IBookAppoinment:IGeneric<BookedAppointments>
    {
        Task<int> FindTimeInterval(Int64 DoctorClinicSessionID);
        Task<List<BookedAppointments>> GetAllBookedAppointments(Int64 doctorid,Int64 sessionid);
        Task<List<DailyCollectionDto>> DailyColle(DateTime date,Int64 doctorid);
        Task<List<BookedAppointments>>GetAppo(DateTime Fromdate, DateTime Todate,Int64 doctorid);

    }
}
