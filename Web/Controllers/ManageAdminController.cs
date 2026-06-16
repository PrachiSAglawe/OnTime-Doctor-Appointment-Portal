using Core;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    
    public class ManageAdminController : Controller
    {
        IAdmin repo;

        public ManageAdminController(IAdmin repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto rec)
        {
            if (ModelState.IsValid)
            {
                var res=await this.repo.Login(rec);

                if (res.IsLoggedIn)
                {
                    HttpContext.Session.SetString("AdminID", res.LoggedInID.ToString());
                    HttpContext.Session.SetString("FirstName", res.LoggeedInName.ToString());
                    return RedirectToAction("Index","AdminHome", new {area="AdminArea"});
                }
                else
                {
                    ModelState.AddModelError("",res.Message);
                    return View(rec);
                }
            }
            return View(rec);
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        
        }
    }
}
