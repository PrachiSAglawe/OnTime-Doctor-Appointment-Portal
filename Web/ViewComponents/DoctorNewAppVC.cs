using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Web.ViewComponents
{
    public class DoctorNewAppVC:ViewComponent
    {
        IBookAppoinment bookapp;
        public DoctorNewAppVC(IBookAppoinment bookapp)
        {
            this.bookapp = bookapp;
        }
        public async Task<IViewComponentResult> InvokeAsync(Int64 doctorid,Int64 sessionid)
        {
            var res= await this.bookapp.GetAllBookedAppointments(doctorid,sessionid);   
            return View(res);
        }
    }
}
