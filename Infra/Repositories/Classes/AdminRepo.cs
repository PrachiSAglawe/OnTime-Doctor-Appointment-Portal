using Core;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Classes
{
    public class AdminRepo : IAdmin
    {
        HospitalContext db;

        public AdminRepo(HospitalContext db)
        {
            this.db = db;
        }

        public  async Task<RepoResultDto> ChangePassword(ChangePasswordDto rec, long id)
        {

            var oldrec = await this.db.Admins.FindAsync(id);
            RepoResultDto res= new RepoResultDto();
            if (oldrec.Password == rec.OldPassword)
            {
                if (rec.NewPassword == rec.RetypePassword)
                {
                    oldrec.Password = rec.NewPassword;
                    this.db.SaveChanges();
                    res.IsSuccess = true;
                    res.Message = "Password Change Successfully..!";

                }
                else
                {
                    res.IsSuccess= false;
                    res.Message = "New and Retype Password are not same..!";
                }
               
            }
            else
            {
                res.IsSuccess = false;
                res.Message = "Password and old Password are not same..!";
            }
            return res;
        }

        public async Task<LoginResultDto> Login(LoginDto rec)
        {
            var oldrec= await this.db.Admins.SingleOrDefaultAsync(p=>p.EmailID==rec.EmailID && p.Password==rec.Password) ;
            
            LoginResultDto resultDto= new LoginResultDto();

            if (oldrec != null)
            {
                resultDto.IsLoggedIn = true;
                resultDto.LoggedInID = oldrec.AdminID;
                resultDto.LoggeedInName = oldrec.FirstName;

            }
            else
            {
                resultDto.IsLoggedIn= false;
                resultDto.Message = "Invalid Email ID and Password";
            }
            return resultDto;
        }
        public async Task<List<ClinicReportAdDto>> GetAppoiCount(DateTime Fromdate, DateTime Todate)
        {
            var res = from t in db.BookedAppointments
                     
                      join t1 in db.DoctorClinicSession
                      on t.DoctorClinicSessionID equals t1.DoctorClinicSessionID 
                      where t.AppointmentDate.Date >= Fromdate.Date && t.AppointmentDate.Date <= Todate.Date
                      group t by t1.Clinic.ClinicName into g
                      select new ClinicReportAdDto
                      {
                          ClinicName=g.Key,
                          TotalAppointment=g.Count()
                      };




            return  await res.ToListAsync();
        }

        public async Task<List<ClinicReportAdDto>> GetClinicAmount(DateTime Fromdate, DateTime Todate)
        {
            

            var res = from t in db.BookedAppointments
                      join t1 in db.BookedAppPayment
                      on t.BookedAppointmentsID equals t1.BookedAppointmentsID
                      where t.AppointmentDate.Date >=Fromdate.Date && t.AppointmentDate.Date <=Todate.Date
                      join t2 in db.DoctorClinicSession
                      on t.DoctorClinicSessionID equals t2.DoctorClinicSessionID
                      group t1 by t2.Clinic.ClinicName into g
                      select new ClinicReportAdDto
                      {
                          ClinicName= g.Key,
                          TotalAmount=g.Sum(p=>p.Amount)
                      };
            return await res.ToListAsync();
        }
    }
}
