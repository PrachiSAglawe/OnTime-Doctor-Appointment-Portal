using Core;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers
{
    public class ManageClinicOwnerController : Controller
    {
        IClinicOwner repo;
        IClinic Clinic;
        ICity city;
        ICountry country;
        IState state;
        public ManageClinicOwnerController(IClinicOwner repo,IClinic Clinic, ICity city,IState state,ICountry country)
        {
            this.repo = repo;
            this.Clinic = Clinic;
            this.city = city;
            this.country = country;
            this.state = state;
        }
        [HttpGet]
        public async Task<IActionResult> SignUp()
         {
            ViewBag.Clinics = new SelectList(await this.Clinic.GetAll(), "ClinicID", "ClinicName");
            ViewBag.cities = new SelectList(await this.city.GetAll(), "CityID", "CityName");
            ViewBag.states = new SelectList(await this.state.GetAll(), "StateID", "StateName");
            ViewBag.contries = new SelectList(await this.country.GetAll(), "CountryID", "CountryName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(ClinicOwnerSignUpDto rec)
        {
            ViewBag.Clinics = new SelectList(await this.Clinic.GetAll(), "ClinicID", "CliniName");
            ViewBag.cities = new SelectList(await this.city.GetAll(), "CityID", "CityName");
            ViewBag.states = new SelectList(await this.state.GetAll(), "StateID", "StateName");
            ViewBag.contries = new SelectList(await this.country.GetAll(), "CountryID", "CountryName");
            if (ModelState.IsValid)
            {
                var res = await this.repo.SignUp(rec);
                TempData["Message"] = res.Message;
                if (res.IsSuccess)
                {
                    return RedirectToAction("SignIn");
                }
                else
                {
                    ModelState.AddModelError("", res.Message);
                    return View(rec);
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
            if (ModelState.IsValid) { 
               var res = await this.repo.SingIn(rec);
                if (res.IsLoggedIn)
                {
                    HttpContext.Session.SetString("ClinicOwnerID", res.LoggedInID.ToString());
                    HttpContext.Session.SetString("FullName",res.LoggeedInName.ToString());
                    return RedirectToAction("Index", "ClinicIndexHome", new { area = "ClinicArea" });
                }
                else
                {
                    ModelState.AddModelError("", res.Message);
                }
            
            }
            return View(rec) ;
        }
        [HttpGet]
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }
        [HttpGet]
        public async Task<IActionResult> GetstateJson(Int64 CountryID)
        {
           var rec=await this.state.GetStatebyID(CountryID);
            return Json(rec);
        }
        [HttpGet]
        public async Task<IActionResult> GetCityJson(Int64 stateid)
        {
            var res= await this.city.GetCityByStateID(stateid);
            return Json(res);
        }
    }
}
