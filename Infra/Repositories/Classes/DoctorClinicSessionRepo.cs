using Core;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Classes
{
    public class DoctorClinicSessionRepo:GenericRepo<DoctorClinicSession>,IDoctorClinicSession
    {
        HospitalContext hospitalContext;
        public DoctorClinicSessionRepo(HospitalContext hospitalContext) : base(hospitalContext)
        {
            this.hospitalContext = hospitalContext;
        }

        public async Task<RepoResultDto> BookAppointmentPatient(BookAppointmentDto rec)
        {
            RepoResultDto result = new RepoResultDto();
            try
            {
                Patient patient = new Patient();



                patient.FirstName = rec.FirstName;
                patient.LastName = rec.LastName;
                patient.Address = rec.Address;
                patient.MobileNo = rec.MobileNo;
                patient.Gender = rec.Gender;
                patient.EmailID = rec.EmailID;
                patient.PhotoPath = rec.PhotoPath;
                patient.UserID = rec.UserID;


                BookedAppointments BA = new BookedAppointments();


                BA.AppointmentDate = rec.AppointmentDate;
                BA.StartTime = rec.StartTime;
                BA.EndTime = rec.EndTime;
                BA.DoctorClinicSessionID = rec.DoctorClinicSessionID;
             

                BA.IsPaid = rec.IsPaid;

                patient.BookedAppointments.Add(BA);
                await this.hospitalContext.Patients.AddAsync(patient);
                await this.hospitalContext.SaveChangesAsync();
                 

                
               
               
                result.IsSuccess = true;
                result.Message = "Appointment Booked Successfully...!";


            }
            catch (Exception ex) { 
              result.IsSuccess=false;
                result.Message = ex.Message;
            
            }
            return result;
         }

        //public string GetEndTime(long DoctorClinicSessionID)
        //{
        //    var res = from t in hospitalContext.DoctorClinicSession where t.
        //}
    }
}
