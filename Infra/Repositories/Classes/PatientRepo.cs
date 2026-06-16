using Core;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Classes
{
    public class PatientRepo:GenericRepo<Patient>,IPatient
    {
        HospitalContext hospitalContext;
        public PatientRepo(HospitalContext hospitalContext) : base(hospitalContext) {
            this.hospitalContext = hospitalContext;
        
        }

        public async Task<List<Patient>> GetAllPatient(DateTime Fromdate, DateTime Todate, long DID)
        {
            var res = from t in this.hospitalContext.BookedAppointments
                      join t1 in this.hospitalContext.Patients
                      on t.PatientID equals t1.PatientID
                      join t2 in this.hospitalContext.DoctorClinicSession
                      on t.DoctorClinicSessionID equals t2.DoctorClinicSessionID
                      where t2.Doctor.DoctorID == DID && t.AppointmentDate.Date>=Fromdate.Date && t.AppointmentDate.Date <=Todate.Date
                      select t1;
            return await res.ToListAsync();
       }

        public  Task<PatientInfoDto> GetPatientInfo(long Patientid)
        {
            var res = (from t in this.hospitalContext.Patients
                      where t.PatientID == Patientid
                      select new PatientInfoDto
                      {
                          FullName = t.FullName,
                          Address = t.Address,
                          MobileNo = t.MobileNo,
                          Gender = t.Gender,
                          EmailID = t.EmailID,
                      }).FirstOrDefaultAsync();
            return res;
                        
        }
        public async Task<List<PatientCollectionDto>> GetPatientAmount(DateTime Fromdate, DateTime Todate, long DID)
        {
            var res = from t in this.hospitalContext.BookedAppointments
                      join t1 in this.hospitalContext.Patients
                      on t.PatientID equals t1.PatientID
                      join t2 in this.hospitalContext.BookedAppPayment
                      on t.BookedAppointmentsID equals t2.BookedAppointmentsID
                      join t3 in this.hospitalContext.DoctorClinicSession
                      on t.DoctorClinicSessionID equals t3.DoctorClinicSessionID
                      where t.AppointmentDate.Date >= Fromdate.Date && t.AppointmentDate.Date <= Todate.Date && t3.Doctor.DoctorID==DID
                      select new PatientCollectionDto
                      {
                          PatientName=t1.FullName,
                          Amount=t2.Amount
                      };
            return await res.ToListAsync();
        }

    }
}
