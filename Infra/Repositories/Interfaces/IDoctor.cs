using Core;
using Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Interfaces
{
    public interface IDoctor:IGeneric<Doctor>
    {

        Task<Doctor> GetByIdDetails(Int64 id);
        Task<DoctorDto> GetDoctorByID(Int64 id);

        //Checkbox
        Task<RepoResultDto>AddDoctor(Doctor doctor, Int64[] Specility);
        Task<RepoResultDto> EditDoctor(Doctor doctor, Int64[] Specility);

        
        Task<RepoResultDto> SignUp(DoctorSignUpDto rec);
        Task<LoginResultDto> SignIn(DoctorLoginDto login);
        Task<RepoResultDto> EditProfile(Doctor rec,Int64 id);
        Task<Doctor> GetDoctorByIDEdit(Int64 id);

        Task<RepoResultDto> ChangePassword(ChangePasswordDto rec,Int64 id);

        Task<List<DoctorInfoDto>>SearchDoctor(Int64 SID,Int64 CID );
        Task<List<DoctorInfoDto>>GetAllDoctor();
        Task<List<DoctorInfoDto>> SearchDoctorAreaSpe(Int64 AID, Int64 SID);
        Task<DoctorInfoDto>DoctorClinicInfo(Int64 DoctorID);
        Task<DoctorPrescribeInfoDto> DoctorPrescribedInfo(Int64 DoctorID);
        Task<List<ClinicCasDto>> GetClinicByID(long doctorid);
    }
}
