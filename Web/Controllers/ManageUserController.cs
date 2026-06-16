using Core;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers
{
    public class ManageUserController : Controller
    {
        ICountry country;
        IUser repo;

        public ManageUserController(ICountry country,IUser repo)
        {
            this.country = country;
            this.repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            ViewBag.Countries = new SelectList(await this.country.GetAll(), "CountryID", "CountryName");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpDto rec)
        {
            ViewBag.Countries = new SelectList(await this.country.GetAll(), "CountryID", "CountryName");
            if (ModelState.IsValid)
            {
                var res= await this.repo.SignUp(rec);
                if(res.IsSuccess)
                {
                    return RedirectToAction("SignIn");
                }
                else
                {
                    ModelState.AddModelError("",res.Message);
                    return View(res);
                }
            }
            
           return View(rec);
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> SignIn(LoginDto rec)
        {
            if (ModelState.IsValid)
            {
                var res = await this.repo.SingIn(rec);
                if (res.IsLoggedIn)
                {
                    HttpContext.Session.SetString("UserID",res.LoggedInID.ToString());
                    HttpContext.Session.SetString("FullName",res.LoggeedInName.ToString());
                    return RedirectToAction("Index", "UserHome", new { area = "UserArea" });
                }
                else
                {
                    ModelState.AddModelError("", res.Message);
                }
            }
            return View(rec) ;
        }
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }
    }
}
