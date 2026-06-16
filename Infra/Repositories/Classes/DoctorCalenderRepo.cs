using Core;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Classes
{
    public class DoctorCalenderRepo:GenericRepo<DoctorCalender>,IDoctorCalender
    {
        HospitalContext hospitalContext;
        public DoctorCalenderRepo(HospitalContext hospitalContext) : base(hospitalContext)
        {
            this.hospitalContext = hospitalContext;
        }

       

        public async Task<List<DoctorSessionDto>> GetSessionName()
        {
            var res = from t in this.hospitalContext.DoctorClinicSession
                       join t1 in this.hospitalContext.OPDSessions
                       on t.OPDSessionID equals t1.OPDSessionID
                      select new DoctorSessionDto
                      {
                          DoctorClinicSessionID = t.DoctorClinicSessionID,
                          SessionName = t1.SessionName,

                      };
            return await res.ToListAsync();


            

        }
        public async Task<List<GetSessionbyDIDDto>> GetSessionByDoctorID(long doctorID)
        {
            var res = from t in this.hospitalContext.DoctorClinicSession
                      join t1 in this.hospitalContext.OPDSessions
                      on t.OPDSessionID equals t1.OPDSessionID
                      where t.DoctorID == doctorID
                      select new GetSessionbyDIDDto
                      {
                          DoctorClinicSessionID = t.DoctorClinicSessionID,
                          SessionName=t.OPDSession.SessionName,
                          
                      };
            return await res.ToListAsync();
        }

        public async Task<List<string>> GetAvailableDates(long doctorId)
            {
            var calendars = (from t in hospitalContext.DoctorCalenders
                             join t1 in hospitalContext.DoctorClinicSession
                                 on t.DoctorClinicSessionID equals t1.DoctorClinicSessionID
                             join t3 in hospitalContext.doctors
                                 on t1.DoctorID equals t3.DoctorID
                             where t3.DoctorID == doctorId
                             select new DoctorAvailableDateDto
                             {
                                 FromDate = t.Fromdate,
                                 ToDate = t.Todate
                             }).ToList();

           
            var availableDates = new HashSet<string>();

            foreach (var item in calendars)
            {
                var start = item.FromDate.Date;
                var end = item.ToDate.Date;

                for (var dt = start; dt <= end; dt = dt.AddDays(1))
                {
                    availableDates.Add(dt.ToString("yyyy-MM-dd"));
                }
            }

            return availableDates.ToList();
        }

        public List<string> GetTimeSlots(long sessionId)
        {
            var session = hospitalContext.DoctorClinicSession
            .FirstOrDefault(s => s.DoctorClinicSessionID == sessionId);

            if (session == null)
                return new List<string>();

            // Parse StartTime and EndTime  
            DateTime start = DateTime.Parse(session.StartTime);
            DateTime end = DateTime.Parse(session.EndTime);
            int interval = session.TimeInterval;

            List<string> slots = new List<string>();

            for (DateTime t = start; t < end; t = t.AddMinutes(interval))
            {
                slots.Add(t.ToString("hh:mm tt"));
            }
          
            return  slots;
        }


       
    }
    }




