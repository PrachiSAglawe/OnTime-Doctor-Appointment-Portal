using Core;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.CuthAuth;

namespace Web.Areas.ClinicArea.Controllers
{
    [Area("ClinicArea")]
    [ClinicAuth]
    public class ClinicIndexHomeController : Controller
    {
        IClinicOwner repo;
        public ClinicIndexHomeController(IClinicOwner repo)
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
                Int64 id = Convert.ToInt64(HttpContext.Session.GetString("ClinicOwnerID"));
                var res = await this.repo.ChangePassword(rec, id);
                ModelState.AddModelError("", res.Message);
                return View();
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            Int64 ownerID= Convert.ToInt64(HttpContext.Session.GetString("ClinicOwnerID"));
            var rec = await this.repo.GetUserByID(ownerID);

            return View(rec);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(ClinicOwner rec)
        {
            Int64 ownerID = Convert.ToInt64(HttpContext.Session.GetString("ClinicOwnerID"));
            var res = await this.repo.EditProfile(rec, ownerID);
            ModelState.AddModelError("", res.Message);
            return View(rec);
        }
    }
}
