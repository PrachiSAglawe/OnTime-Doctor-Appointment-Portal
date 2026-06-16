using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.ViewComponents
{
    public class DoctorPrescribeInfoVC:ViewComponent
    {
        IDoctor doctor;
        public DoctorPrescribeInfoVC(IDoctor doctor)
        {
            this.doctor = doctor;
        }
        public async Task<IViewComponentResult> InvokeAsync(Int64 doctorId)
        {
            var res = await this.doctor.DoctorPrescribedInfo(doctorId);
            return View(res);  
        }
    }
}
