using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents
{
    public class DoctorClinicVC:ViewComponent
    {
        IDoctor doctor;
        public  DoctorClinicVC(IDoctor doctor)
        {
            this.doctor = doctor;
        }
        public async Task<IViewComponentResult> InvokeAsync(Int64 id)
        {
            var res= await this.doctor.DoctorClinicInfo(id);
            return View(res);  
        }
    }
}
