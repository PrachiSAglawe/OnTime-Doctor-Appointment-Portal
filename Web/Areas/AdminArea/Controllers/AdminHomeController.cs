using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.CuthAuth;

namespace Web.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [AdminAuth]
    public class AdminHomeController : Controller
    {
        IAdmin repo;
        public AdminHomeController(IAdmin repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto rec)
        {
            if (ModelState.IsValid)
            {
                Int64 id = Convert.ToInt64(HttpContext.Session.GetString("AdminID"));
                var res = await this.repo.ChangePassword(rec, id);
                ModelState.AddModelError("", res.Message);
                return View();
            }
            return View();
            
        }
        [HttpGet]
        public IActionResult PatientReport()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetClinicReport(DateTime Fromdate,DateTime Todate)
        {
            var res = await this.repo.GetAppoiCount(Fromdate,Todate);
            return View(res);
        }

        [HttpGet]
        public IActionResult AmountCollection()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> GetAmountCollection(DateTime Fromdate,DateTime Todate)
        {
            var res = await this.repo.GetClinicAmount(Fromdate,Todate);
            return View(res);
        }
    }
}
