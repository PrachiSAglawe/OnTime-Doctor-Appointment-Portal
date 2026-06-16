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
    public class UserRepo : GenericRepo<User>, IUser
    {
        HospitalContext hospitalContext;

        public UserRepo(HospitalContext hospitalContext) : base(hospitalContext)
        {
            this.hospitalContext = hospitalContext;
        }


        public async Task<RepoResultDto> SignUp(UserSignUpDto rec)
        {
            RepoResultDto result = new RepoResultDto();

            try
            {
                User urec = new User()
                {
                    FirstName = rec.FirstName,
                    LastName = rec.LastName,
                    CountryID = rec.CountryID,
                    EmailID = rec.EmailID,
                    MobileNo = rec.MobileNo,
                    Address = rec.Address,
                    Password = rec.Password,
                };
                await hospitalContext.Users.AddAsync(urec);
                await hospitalContext.SaveChangesAsync();
                result.IsSuccess = true;
                result.Message = "User Signed Up";

            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Message = e.Message;
            }
            return result;
        }

        public async Task<LoginResultDto> SingIn(LoginDto rec)
        {
            LoginResultDto result = new LoginResultDto();
            var oldrec = await this.hospitalContext.Users.FirstOrDefaultAsync(p => p.EmailID == rec.EmailID && p.Password == rec.Password);
            if (oldrec != null)
            {
                result.IsLoggedIn = true;
                result.LoggeedInName = oldrec.FullName;
                result.LoggedInID = oldrec.UserID;


            }
            else
            {
                result.IsLoggedIn = false;
                result.Message = "Invalid EmailID and Password";
            }
            return result;
        }

        public async Task<RepoResultDto> ChangePassword(ChangePasswordDto rec, Int64 id)
        {
            RepoResultDto res = new RepoResultDto();
            var oldrec = await this.hospitalContext.Users.FindAsync(id);
            if (oldrec.Password == rec.OldPassword)
            {
                if (rec.NewPassword == rec.RetypePassword)
                {

                    oldrec.Password = rec.NewPassword;
                    this.hospitalContext.SaveChanges();
                    res.IsSuccess = true;
                    res.Message = "Password Changed Successfully!";
                }
                else
                {
                    res.IsSuccess = false;
                    res.Message = "New and Retype Password are not Same!";
                }
            }
            else
            {

                res.IsSuccess = false;
                res.Message = "Invalid Old Password";
            }

            return res;
        }


        public async Task<RepoResultDto> EditProfile(User rec, Int64 id)
        {
            RepoResultDto rep = new RepoResultDto();
            try
            {
                var oldrec = await this.hospitalContext.Users.FindAsync(id);

                oldrec.MobileNo = rec.MobileNo;
                oldrec.Address = rec.Address;

                await this.hospitalContext.SaveChangesAsync();
                rep.IsSuccess = true;
                rep.Message = "Profile Edited Successfully!";
                return rep;
            }
            catch (Exception ex)
            {
                rep.IsSuccess = false;
                rep.Message = ex.Message;
                return rep;
            }

        }



        public async Task<User> GetUserByID(long id)
        {
            return await this.hospitalContext.Users.FindAsync(id);
        }

        public async Task<List<BookedAppointments>> GetUserAppoint(long Userid)
        {
            var rec = from t in this.hospitalContext.Patients
                      join t1 in this.hospitalContext.BookedAppointments
                      on t.PatientID equals t1.PatientID
                      where t.UserID == Userid
                      select t1;

            return await rec.ToListAsync();



        }

        public async Task<Prescription> GetPrescribtionID(long BID)
        {
            return await this.hospitalContext.Prescription.FirstOrDefaultAsync(p => p.BookedAppointmentsID == BID);
                                
        }

        public async Task<List<Patient>> GetPatient(long UID)
        {
            var res = this.hospitalContext.Patients
                      .Where(p => p.UserID == UID);

            return await res.ToListAsync();
        }

        public async Task<List<PaymentHistoryDto>> GetPaymentHistory(long UID)
        {
            var res = from t in this.hospitalContext.BookedAppPayment
                            join t1 in this.hospitalContext.BookedAppointments
                            on t.BookedAppointmentsID equals t1.BookedAppointmentsID
                            join t2 in this.hospitalContext.Patients
                            on t1.PatientID equals t2.PatientID
                            join t3 in this.hospitalContext.Prescription
                            on t.BookedAppointmentsID equals t3.BookedAppointmentsID
                            where t2.UserID == UID
                            select new PaymentHistoryDto
                            {
                              DoctorName=t1.DoctorClinicSession.Doctor.FullName,
                              PatientName=t2.FullName,
                              PaymentDate=t3.PrescriptionDate,
                              Amount =t.Amount,
                              PaymentMode=t.PaymentMode

                            };
            return await res.ToListAsync();
        }
    }
}