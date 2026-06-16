using Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Interfaces
{
    public interface IAdmin
    {
      Task<LoginResultDto>Login(LoginDto rec);
        Task<RepoResultDto> ChangePassword(ChangePasswordDto rec, Int64 id);
        Task<List<ClinicReportAdDto>> GetAppoiCount(DateTime Fromdate,DateTime Todate);
        Task<List<ClinicReportAdDto>> GetClinicAmount(DateTime Fromdate,DateTime Todate);
    }
}
