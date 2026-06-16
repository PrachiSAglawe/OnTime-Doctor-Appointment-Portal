using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents
{
    
    public class SearchdoctorInfoVC:ViewComponent
    {
        IDoctor doctor;

        public SearchdoctorInfoVC(IDoctor doctor)
        {
            this.doctor = doctor;
        }
        public async Task<IViewComponentResult> InvokeAsync(Int64 SpecilityID, Int64 CityID)
        {
            var res = await this.doctor.SearchDoctor(SpecilityID, CityID);
            return View(res);
        }
    }
}
