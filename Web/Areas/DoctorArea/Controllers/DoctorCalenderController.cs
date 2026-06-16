using Core;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Web.CuthAuth;

namespace Web.Areas.DoctorArea.Controllers
{
    [Area("DoctorArea")]
    [DoctorAuth]
    public class DoctorCalenderController : Controller
    {
        IDoctorCalender repo;
        IDoctorClinicSession session;
        IOPDSession opdsession;
        
       public DoctorCalenderController(IDoctorCalender repo,IDoctorClinicSession session,IOPDSession opdsession)
        {
            this.repo = repo;
            this.session = session;
            this.opdsession = opdsession;
        }

       
      

        public async Task<IActionResult> Index()
        {
            var res= await this.repo.GetAll();
            return View(res);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.OPDSession=new SelectList(await this.repo.GetSessionName(), "DoctorClinicSessionID", "SessionName");
            
          
            return View();
        }
        public async Task<IActionResult> Create(DoctorCalender rec)
        {
            ViewBag.OPDSession = new SelectList(await this.repo.GetSessionName(), "DoctorClinicSessionID", "SessionName");
            if (ModelState.IsValid)
            {
                var res = await this.repo.Insert(rec);
                TempData["Message"] = res.Message;
                if (res.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(rec);
        }
    }
}
