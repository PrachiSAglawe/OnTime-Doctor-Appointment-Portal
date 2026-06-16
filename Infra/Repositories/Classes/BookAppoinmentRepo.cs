using Core;
using Infra.Dtos;

using Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Classes
{
    public class BookAppoinmentRepo:GenericRepo<BookedAppointments> ,IBookAppoinment
    {
        HospitalContext hospitalContext;
        public BookAppoinmentRepo(HospitalContext hospitalContext) : base(hospitalContext)
        {
            this.hospitalContext = hospitalContext;
        }

        public async Task<int> FindTimeInterval(long DoctorClinicSessionID)
        {
            var session = await hospitalContext.DoctorClinicSession
                                   .FirstOrDefaultAsync(s => s.DoctorClinicSessionID == DoctorClinicSessionID);

            if (session == null)
                throw new Exception("Session not found");

            return session.TimeInterval;
        }

        public  Task<List<BookedAppointments>> GetAllBookedAppointments(long doctorid,long sessionid)
        {
            var res = from t in this.hospitalContext.BookedAppointments
                      where !t.Prescriptions.Any(p=>p.BookedAppointmentsID==t.BookedAppointmentsID)
                      && t.AppointmentDate.Date == DateTime.Today
                      join t1 in this.hospitalContext.DoctorClinicSession
                      on t.DoctorClinicSessionID equals t1.DoctorClinicSessionID
                      where t1.DoctorID == doctorid && t1.OPDSessionID==sessionid
                      select t;
            return res.ToListAsync();
        }

        public async Task<List<DailyCollectionDto>> DailyColle(DateTime date,long doctorid)
        {
            var res =from t in this.hospitalContext.BookedAppointments
                     join t1 in this.hospitalContext.BookedAppPayment
                     on t.BookedAppointmentsID equals t1.BookedAppointmentsID
                     join t2 in this.hospitalContext.DoctorClinicSession
                     on t.DoctorClinicSessionID equals t2.DoctorClinicSessionID
                     where t.AppointmentDate.Date == date.Date && t2.Doctor.DoctorID==doctorid
                     select new DailyCollectionDto
                     {
                         AppointmentDate =t.AppointmentDate,
                         PatientName=t.Patient.FullName,
                         Amount=t1.Amount,
                         PaymentMode=t1.PaymentMode,
                     };
            return await res.ToListAsync();

        }

        public async Task<List<BookedAppointments>>GetAppo(DateTime Fromdate, DateTime Todate,long doctorid)
        {

            var res = from t in this.hospitalContext.BookedAppointments
                      where t.AppointmentDate.Date >= Fromdate.Date
                     && t.AppointmentDate.Date <= Todate.Date
                     && !hospitalContext.Prescription
                        .Any(p => p.BookedAppointmentsID == t.BookedAppointmentsID)
                      join t1 in this.hospitalContext.DoctorClinicSession
                      on t.DoctorClinicSessionID equals t1.DoctorClinicSessionID
                      where t1.Doctor.DoctorID == doctorid

                      select t;



            return await res.ToListAsync();
        }
    }
}
