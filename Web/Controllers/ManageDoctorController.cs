using Core;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mono.TextTemplating;
using ServiceStack.Text;
using System.Threading.Tasks;

namespace Web.Controllers
{



    public class ManageDoctorController : Controller
    {
        ICity city;
        ICountry country;
        IState state;
        IDoctor doctor;
        IWebHostEnvironment env;
        IArea area;
        IClinic Clinic;
        IOPDSession session;

        public ManageDoctorController(IDoctor doctor, ICity city, ICountry country, IState state, IWebHostEnvironment env, IArea area,IClinic clinic,IOPDSession session)
        {
            this.doctor = doctor;
            this.city = city;
            this.country = country;
            this.state = state;
            this.env = env;
            this.area = area;
            this.Clinic = clinic;
           this.session = session;
        }




        [HttpGet]
        public async Task< IActionResult> SignUp()
        {

            ViewBag.Areas = new SelectList(await this.area.GetAll(), "AreaID", "AreaName");
            ViewBag.countries = new SelectList(await this.country.GetAll(), "CountryID", "CountryName");
            ViewBag.states = new SelectList(await this.state.GetAll(), "StateID", "StateName");
            ViewBag.cities = new SelectList(await this.city.GetAll(), "CityID", "CityName");
            ViewBag.clinics = new SelectList(await this.Clinic.GetAll(), "ClinicID", "ClinicName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(DoctorSignUpDto rec)
        {
            ViewBag.Areas = new SelectList(await this.area.GetAll(), "AreaID", "AreaName");
            ViewBag.countries = new SelectList(await this.country.GetAll(), "CountryID", "CountryName");
            ViewBag.states = new SelectList(await this.state.GetAll(), "StateID", "StateName");
            ViewBag.cities = new SelectList(await this.city.GetAll(), "CityID", "CityName");
            ViewBag.clinics = new SelectList(await this.Clinic.GetAll(), "ClinicID", "ClinicName");


            if (ModelState.IsValid)
            {
                if (rec.PhotoFile != null)
                {
                    if (rec.PhotoFile.Length > 0)
                    {
                        //logical path
                        string logpath = "DoctorsPhoto";

                        string folderpath = Path.Combine(env.WebRootPath, logpath);
                        string filepath = Path.Combine(folderpath, rec.PhotoFile.FileName);

                        FileStream fs = new FileStream(filepath, FileMode.Create);
                        await rec.PhotoFile.CopyToAsync(fs);
                        fs.Close();
                        rec.PhotoPath = "/DoctorsPhoto/" + rec.PhotoFile.FileName;
                    }
                    else
                    {
                        ModelState.AddModelError("PhotoFile", "Please Select File");
                        return View(rec);
                    }
                }
               
                var res = await this.doctor.SignUp(rec);
                TempData["Message"] = res.Message;
                if (res.IsSuccess)
                {
                    return RedirectToAction("SignIn");
                }
            }

            return View(rec);
        }

        [HttpGet]
        public async Task<IActionResult> SignIn()
        {
            ViewBag.SessionName = new SelectList(await this.session.GetAll(), "OPDSessionID", "SessionName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(DoctorLoginDto rec)
        {
            ViewBag.SessionName = new SelectList(await this.session.GetAll(), "OPDSessionID", "SessionName");

            
            
            if (ModelState.IsValid) {
               var res=await this.doctor.SignIn(rec);
               HttpContext.Session.SetString("SessionName", rec.SessionName.ToString());
                HttpContext.Session.SetString("OpdSessionID", rec.OpdSessionID.ToString());
                ViewBag.dateTime = DateTime.Now;
                if (res.IsLoggedIn)
                {
                    HttpContext.Session.SetString("DoctorID",res.LoggedInID.ToString());
                    HttpContext.Session.SetString("FullName",res.LoggeedInName.ToString());
                    
                    return RedirectToAction("Index", "DoctorHome", new { area = "DoctorArea" });
                }
                else
                {
                    ModelState.AddModelError("", res.Message);
                    return View(rec);
                }

            }
            return View(rec);
           

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }
        [HttpGet]
      
        public async Task<IActionResult> GetstateJson(Int64 CountryID)
        {
            var res = await this.state.GetStatebyID(CountryID);
            return Json(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetCityJson(Int64 stateid)
        {
            var res =await this.city.GetCityByStateID(stateid);
            return Json(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetAreaJSON(Int64 cityid)
        {
            var res= await this.area.GetAreaByCityID(cityid);
            return Json(res);
        }




    }
    }

