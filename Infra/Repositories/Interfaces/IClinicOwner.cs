using Core;
using Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Interfaces
{
    public interface IClinicOwner
    {
        Task<RepoResultDto> SignUp(ClinicOwnerSignUpDto rec);
        Task<LoginResultDto> SingIn(LoginDto rec);
        Task<RepoResultDto> ChangePassword(ChangePasswordDto rec, Int64 id);
        Task<RepoResultDto> EditProfile(ClinicOwner rec, Int64 id);
        Task<ClinicOwner> GetUserByID(Int64 id);
    }
}
