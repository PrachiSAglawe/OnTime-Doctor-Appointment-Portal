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
    public class ClinicOwnerRepo:IClinicOwner
    {
        HospitalContext db;
        public ClinicOwnerRepo(HospitalContext db)
        {
            this.db = db;
        }
        public async Task<RepoResultDto> SignUp(ClinicOwnerSignUpDto rec)
        {
            RepoResultDto result = new RepoResultDto();
            try
            {
                Clinic Clinicrec = new Clinic();

                Clinicrec.ClinicName = rec.ClinicName;
                Clinicrec.Address = rec.Address;
                Clinicrec.MobileNo = rec.MobileNo;
                Clinicrec.EmailID = rec.EmailID;
                Clinicrec.ContactPersonName = rec.ContactPersonName;
                Clinicrec.LandLineNo = rec.LandLineNo;
                Clinicrec.CityID = rec.CityID;

                ClinicOwner Crec = new ClinicOwner();


                Crec.Gender = rec.Gender;
                Crec.EmailID = rec.EmailID;
                Crec.FirstName = rec.FirstName;
                Crec.LastName = rec.LastName;
                Crec.MobileNo = rec.MobileNo;
                Crec.Password = rec.Password;
               
                Clinicrec.ClinicOwner.Add(Crec);
                await this.db.Clinics.AddAsync(Clinicrec);
                await this.db.SaveChangesAsync();


               
                result.IsSuccess = true;
                result.Message = "Clinic Owner Singned Up!!!!";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<LoginResultDto> SingIn(LoginDto rec)
        {
            LoginResultDto result = new LoginResultDto();
            var oldrec = await this.db.ClinicOwner.SingleOrDefaultAsync(p => p.EmailID == rec.EmailID && p.Password == rec.Password);

            if (oldrec != null)
            {
                result.IsLoggedIn = true;
                result.LoggeedInName=oldrec.FullName;
                result.LoggedInID = oldrec.ClinicOwnerID;
            }
            else
            {
                result.IsLoggedIn = false;
                result.Message = "Invalid Email ID or Password!";
            }

            return result;
        }
        public async Task<RepoResultDto> ChangePassword(ChangePasswordDto rec, Int64 id)
        {
            RepoResultDto res = new RepoResultDto();
            var oldrec = await this.db.ClinicOwner.FindAsync(id);
            if (oldrec.Password == rec.OldPassword)
            {
                if (rec.NewPassword == rec.RetypePassword)
                {

                    oldrec.Password = rec.NewPassword;
                    this.db.SaveChanges();
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


        public async Task<RepoResultDto> EditProfile(ClinicOwner rec, Int64 id)
        {
            RepoResultDto rep = new RepoResultDto();
            try
            {
                var oldrec = await this.db.ClinicOwner.FindAsync(id);
                oldrec.FirstName = rec.FirstName;
                oldrec.LastName = rec.LastName;
                oldrec.MobileNo = rec.MobileNo;
              
                await this.db.SaveChangesAsync();
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

        public async Task<ClinicOwner> GetUserByID(long id)
        {
            return await this.db.ClinicOwner.FindAsync(id);
        }



    }
}
