using Core;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Infra.Dtos.DoctorInfoDto;

namespace Infra.Repositories.Classes
{
    public class DoctorRepo:GenericRepo<Doctor>,IDoctor
    {
        HospitalContext hospitalContext;
        public DoctorRepo(HospitalContext hospitalContext):base(hospitalContext)
        {
            this.hospitalContext = hospitalContext;
        }

      
        //Used for details (Action) record 
        public async Task<Doctor> GetByIdDetails(long id)
        {
            var res = await this.hospitalContext.doctors.FindAsync(id);
            return res;
        }
        //edit photo code
        public async Task<DoctorDto> GetDoctorByID(long id)
        {
            var res = from t in hospitalContext.doctors
                      where t.DoctorID == id
                      select new DoctorDto
                      {
                          DoctorID = t.DoctorID,
                          FirstName = t.FirstName,
                          LastName = t.LastName,
                          MobileNo = t.MobileNo,
                          IsAvailable = t.IsAvailable,
                          Address = t.Address,
                          DoctorExperience = t.DoctorExperience,
                          AreaID = t.AreaID,
                          PhotoPath = t.PhotoPath,
                          DoctorQualification = t.DoctorQualification,
                         Password=t.Password,
                          StateID=t.Area.City.StateID,
                          CityID=t.Area.CityID,
                          ClinicID=t.ClinicID,
                          OPDFees=t.OPDFees,

                          CountryID=t.Area.City.State.CountryID,

                      };
            return await res.FirstOrDefaultAsync(); 
        }
        //Checkbox nd add data inside 2 diff table
        public async Task<RepoResultDto> AddDoctor(Doctor doctor, long[] Specility)
        {
           RepoResultDto res= new RepoResultDto();
            try
            {
                foreach (var temp in Specility)
                {
                    DoctorSpeciality dc = new DoctorSpeciality();
                    dc.SpecilityID = temp;
                    doctor.DoctorSpecialities.Add(dc);
                }
                await this.hospitalContext.doctors.AddAsync(doctor);
                await this.hospitalContext.SaveChangesAsync();
                res.IsSuccess = true;
                res.Message = "Doctor Created";

            }
            catch (Exception ex) { 
            res.IsSuccess= false;
            res.Message= ex.Message;
            
            }
            return res;
        }
        //Checkbox
        public async Task<RepoResultDto> EditDoctor(Doctor doctor, long[] Specility)
        {
           RepoResultDto res=new RepoResultDto();

            try
            {
                //delete old record
                var prodSpecility = this.hospitalContext.DoctorSpecialities.Where(p => p.DoctorID ==doctor.DoctorID);
                this.hospitalContext.DoctorSpecialities.RemoveRange(prodSpecility);
                await this.hospitalContext.SaveChangesAsync();

                var oldSpecility = await this.hospitalContext.doctors.FindAsync(doctor.DoctorID);

                oldSpecility.FirstName = doctor.FirstName;
                oldSpecility.LastName = doctor.LastName;
                oldSpecility.MobileNo = doctor.MobileNo;
                oldSpecility.IsAvailable = doctor.IsAvailable;
                oldSpecility.Address = doctor.Address;
                oldSpecility.OPDFees = doctor.OPDFees;
                oldSpecility.DoctorExperience = doctor.DoctorExperience;
                oldSpecility.AreaID = doctor.AreaID;
                oldSpecility.PhotoPath = doctor.PhotoPath;
                oldSpecility.DoctorQualification = doctor.DoctorQualification;
                oldSpecility.Password = doctor.Password;
                oldSpecility.ClinicID = doctor.ClinicID;
                
                foreach (var temp in Specility)
                {
                    DoctorSpeciality ds = new DoctorSpeciality();
                    ds.SpecilityID = temp;
                    ds.DoctorID = doctor.DoctorID;
                    oldSpecility.DoctorSpecialities.Add(ds);
                }


                await this.hospitalContext.SaveChangesAsync();
                res.IsSuccess = true;
                res.Message = "Doctor Update!";
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
            }

            return res;

        }

        public async Task<RepoResultDto> SignUp(DoctorSignUpDto rec)
        {
            RepoResultDto result= new RepoResultDto();

            try
            {
                Doctor Drec = new Doctor()
                {
                    FirstName = rec.FirstName,
                    LastName = rec.LastName,
                    MobileNo = rec.MobileNo,
                    IsAvailable = rec.IsAvailable,
                    Address = rec.Address,
                    DoctorExperience = rec.DoctorExperience,
                    AreaID = rec.AreaID,
                    PhotoPath = rec.PhotoPath,
                    DoctorQualification = rec.DoctorQualification,
                    Password = rec.Password,
                    ClinicID = rec.ClinicID,
                    


                 

                };
                 await this.hospitalContext.doctors.AddAsync(Drec);
                await this.hospitalContext.SaveChangesAsync();
                result.IsSuccess = true;
                result.Message = "Doctor Signed Up Successfully..!";
            } 
            catch (Exception ex) {
                result.IsSuccess = false;
                result.Message = ex.Message;
            
            }
            return result;
        }

        public async Task<LoginResultDto> SignIn(DoctorLoginDto login)
        {
           LoginResultDto result = new LoginResultDto();

            var oldrec= await this.hospitalContext.doctors.FirstOrDefaultAsync(p=>p.MobileNo==login.MobileNo && p.Password==login.Password);
            if (oldrec != null) {
                result.IsLoggedIn = true;
                result.LoggeedInName=oldrec.FullName;
                result.LoggedInID=oldrec.DoctorID;

            }
            else
            {
                result.IsLoggedIn = false;
                result.Message = "Invalid MobileNo and Password";
            }
            return result;
        }

        public async Task<RepoResultDto> EditProfile(Doctor rec,long id)
        {
            RepoResultDto result = new RepoResultDto();
            try
            {
                var oldrec = await this.hospitalContext.doctors.FindAsync(id);
                oldrec.FirstName = rec.FirstName;
                oldrec.LastName = rec.LastName;
                oldrec.MobileNo = rec.MobileNo;
                oldrec.IsAvailable = rec.IsAvailable;
                oldrec.Address = rec.Address;
                oldrec.DoctorExperience = rec.DoctorExperience;
               
                oldrec.AreaID = rec.AreaID;
                oldrec.DoctorQualification = rec.DoctorQualification;

                await this.hospitalContext.SaveChangesAsync();
                result.IsSuccess = true;
                result.Message = "Record Edited Successfully..!";
                
                return result;
            }catch(Exception err)
            {
                result.IsSuccess = false;
                result.Message = err.Message;
                return result;
            }
        }

        public async Task<Doctor> GetDoctorByIDEdit(long id)
        {
            var res = await this.hospitalContext.doctors.FindAsync(id);
            return res;
        }

        public async Task<RepoResultDto> ChangePassword(ChangePasswordDto rec, long id)
        {
            RepoResultDto result = new RepoResultDto();
            var oldrec= await this.hospitalContext.doctors.FindAsync(id);

            if (oldrec.Password == rec.OldPassword)
            {
                if (rec.NewPassword == rec.RetypePassword)
                {
                    oldrec.Password = rec.NewPassword;
                    await this.hospitalContext.SaveChangesAsync();
                    result.IsSuccess=true;
                    result.Message = "Password Change Successfully..!";

                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "New Password and RetypePassword are not Same";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Invalid old Password";
            }
            return result;  
        }

        public async Task<List<DoctorInfoDto>> SearchDoctor(long SID, long CID)
        {

            var res = from t in hospitalContext.DoctorSpecialities
                      join t1 in hospitalContext.doctors
                          on t.DoctorID equals t1.DoctorID
                     join t3 in hospitalContext.Clinics
                     on t1.ClinicID equals t3.ClinicID
                     where t.SpecilityID == SID && t1.Area.CityID==CID
                     //&& t3.CityID == CID

                      select new DoctorInfoDto
                      {
                          DoctorName = t1.FullName,
                          PhotoPath = t1.PhotoPath,
                          ClinicName = t3.ClinicName,
                          ClinicAddress = t3.Address,
                          MobileNo = t3.MobileNo,
                          EmailID = t3.EmailID,
                          DoctorID = t1.DoctorID,
                          SpecilityName=t.Specility.SpecilityName
                      };

            return await res.ToListAsync();
        }

        public async Task<List<DoctorInfoDto>>SearchDoctorAreaSpe(long AID, long SID)
        {
            var res = from t in this.hospitalContext.DoctorSpecialities
                      join t1 in this.hospitalContext.doctors on
                      t.DoctorID equals t1.DoctorID
                      join t4 in this.hospitalContext.Clinics on
                      t1.ClinicID equals t4.ClinicID
                      where t1.AreaID == AID && t.SpecilityID == SID

                      select new DoctorInfoDto
                      {
                          DoctorName = t1.FullName,
                          PhotoPath = t1.PhotoPath,
                          ClinicName = t4.ClinicName,
                          ClinicAddress = t4.Address,
                          MobileNo = t4.MobileNo,
                          EmailID = t4.EmailID,
                          DoctorID = t1.DoctorID,
                          SpecilityName = t.Specility.SpecilityName

                      };
            
            return await res.ToListAsync();

        }

        public async Task<List<DoctorInfoDto>> GetAllDoctor()
        {
                var res = (from t in this.hospitalContext.DoctorSpecialities
                          join t1 in this.hospitalContext.doctors on
                          t.DoctorID equals t1.DoctorID
                          join t4 in this.hospitalContext.Clinics on
                          t1.ClinicID equals t4.ClinicID
                          select new DoctorInfoDto
                          {
                              DoctorName = t1.FullName,
                              PhotoPath = t1.PhotoPath,
                              ClinicName = t4.ClinicName,
                              ClinicAddress = t4.Address,
                              MobileNo = t4.MobileNo,
                              EmailID = t4.EmailID,
                              DoctorID = t1.DoctorID,
                              SpecilityName=t.Specility.SpecilityName
                          }).Distinct();
                return await res.ToListAsync();
        }

        public async Task<DoctorInfoDto> DoctorClinicInfo(long DoctorID)
        {
           var res=await(from t in this.hospitalContext.doctors
                      join t1 in this.hospitalContext.Clinics
                      on t.ClinicID equals t1.ClinicID
                      where t.DoctorID == DoctorID
                      select new DoctorInfoDto
                      {
                          DoctorName = t.FullName,
                          ClinicName = t1.ClinicName,
                          ClinicAddress = t1.Address,
                          MobileNo = t1.MobileNo,
                          EmailID = t1.EmailID,
                         
                      }).FirstOrDefaultAsync();
            return res;


        }

        public async Task<DoctorPrescribeInfoDto> DoctorPrescribedInfo(long DoctorID)
        {
            var res = await(from t in this.hospitalContext.doctors
                      join t1 in this.hospitalContext.DoctorSpecialities
                      on t.DoctorID equals t1.DoctorID
                      where t.DoctorID == DoctorID
                      select new DoctorPrescribeInfoDto
                      {
                          DoctorName=t.FullName,
                          DoctorQualification=t.DoctorQualification,
                          MobileNo=t.MobileNo,
                          DoctorSpecilityName=t1.Specility.SpecilityName
                      }).FirstOrDefaultAsync();
            return res;
        }

        //used for cascating
        public async Task<List<ClinicCasDto>> GetClinicByID(long doctorid)
        {
            var res = from t in this.hospitalContext.doctors
                      where t.DoctorID == doctorid
                      select new ClinicCasDto
                      {
                          ClinicID = t.ClinicID,
                          ClinicName = t.Clinic.ClinicName
                      };
            return await res.ToListAsync();
        }
    }

    }










