using Core;
using Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Interfaces
{
   public interface IUser :IGeneric<User>
    {
        Task<RepoResultDto> SignUp(UserSignUpDto rec);
        Task<LoginResultDto> SingIn(LoginDto rec);
        Task<RepoResultDto> ChangePassword(ChangePasswordDto rec, Int64 id);
        Task<RepoResultDto> EditProfile(User rec, Int64 id);
        Task<User> GetUserByID(Int64 id);
        Task<List<BookedAppointments>>GetUserAppoint(Int64 Userid);
        Task<Prescription>GetPrescribtionID(Int64 BID);
        Task<List<Patient>> GetPatient(Int64 UID);
        Task<List<PaymentHistoryDto>>GetPaymentHistory(Int64 UID);
    }
}
