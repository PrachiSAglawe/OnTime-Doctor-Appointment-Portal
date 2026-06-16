using Core;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mono.TextTemplating;
using ServiceStack;
using ServiceStack.Text;
using Web.CuthAuth;

namespace Web.Areas.ClinicArea.Controllers
{
    [Area("ClinicArea")]
    [ClinicAuth]
    public class DoctorHomeController : Controller
    {
        IDoctor repo;
        IArea area;
        ICountry country;
        ICity city;
        IState state;
        IClinic clinic;
        IWebHostEnvironment env;

        public DoctorHomeController(IDoctor repo, IArea area,IWebHostEnvironment env,ICountry country,IState state,ICity city,IClinic clinic)
        {
            this.repo = repo;
            this.area = area;
            this.env = env;
            this.country = country;
            this.city = city;
            this.state = state;
            this.clinic = clinic;
        }
        public async Task<IActionResult> Index()
        {
            var res = await this.repo.GetAll();
            return View(res);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
           ViewBag.Areas=new SelectList(await this.area.GetAll(),"AreaID","AreaName");
            ViewBag.countries = new SelectList(await this.country.GetAll(), "CountryID", "CountryName");
            ViewBag.states = new SelectList(await this.state.GetAll(), "StateID", "StateName");
            ViewBag.cities = new SelectList(await this.city.GetAll(), "CityID", "CityName");
            ViewBag.clinics = new SelectList(await this.clinic.GetAll(), "ClinicID", "ClinicName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Doctor rec, Int64[] Specility)
        {

            ViewBag.Areas = new SelectList(await this.area.GetAll(), "AreaID", "AreaName");
            ViewBag.countries = new SelectList(await this.country.GetAll(), "CountryID", "CountryName");
            ViewBag.states = new SelectList(await this.state.GetAll(), "StateID", "StateName");
            ViewBag.cities = new SelectList(await this.city.GetAll(), "CityID", "CityName");
            ViewBag.clinics = new SelectList(await this.clinic.GetAll(), "ClinicID", "ClinicName");

            if (ModelState.IsValid)
            {
                if (rec.PhotoFile != null)
                {
                    if (rec.PhotoFile.Length > 0)
                    {
                        //logical path
                        string logpath = "DoctorsPhoto";

                        string folderpath = Path.Combine(env.WebRootPath,logpath);
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
                else
                {
                    ModelState.AddModelError("PhotoFile", "Please Select File");
                    return View(rec);
                }


                var res = await this.repo.AddDoctor(rec,Specility);
                TempData["Message"] = res.Message;
                if (res.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(rec);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Int64 id)
        {
            var res = await this.repo.GetDoctorByID(id);
            ViewBag.Areas = new SelectList(await this.area.GetAll(), "AreaID", "AreaName",res.AreaID);
            ViewBag.countries = new SelectList(await this.country.GetAll(), "CountryID", "CountryName",res.CountryID);
            ViewBag.states = new SelectList(await this.state.GetAll(), "StateID", "StateName",res.StateID);
            ViewBag.cities = new SelectList(await this.city.GetAll(), "CityID", "CityName",res.CityID);
            ViewBag.clinics = new SelectList(await this.clinic.GetAll(), "ClinicID", "ClinicName",res.ClinicID);
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult>Edit(Doctor rec, Int64[] Specility )
        {

            ViewBag.Areas = new SelectList(await this.area.GetAll(), "AreaID", "AreaName");
            ViewBag.countries = new SelectList(await this.country.GetAll(), "CountryID", "CountryName");
            ViewBag.states = new SelectList(await this.state.GetAll(), "StateID", "StateName");
            ViewBag.cities = new SelectList(await this.city.GetAll(), "CityID", "CityName");
            ViewBag.clinics = new SelectList(await this.clinic.GetAll(), "ClinicID", "ClinicName");

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
                else
                {
                    ModelState.AddModelError("PhotoFile", "Please Select File");
                    return View(rec);
                }


                var res = await this.repo.EditDoctor(rec, Specility);
                TempData["Message"] = res.Message;
                if (res.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(rec);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Int64 id)
        {
            var res = await this.repo.Delete(id);
            TempData["Message"] = res.Message;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Details(Int64 id)
        {
            var res =await this.repo.GetByIdDetails(id);
            return View(res);
        }

        [HttpGet]
       
        public async Task<IActionResult> GetstateJson(Int64 CountryID)
        {
            var rec = await this.state.GetStatebyID(CountryID);
            return Json(rec);
        }
        [HttpGet]
        public async Task<IActionResult> GetCityJson(Int64 stateid)
        {
            var res = await this.city.GetCityByStateID(stateid);
            return Json(res);
        }
        [HttpGet]

        public async Task<IActionResult> GetAreaJSON(Int64 cityid)
        {
            var res = await this.area.GetAreaByCityID(cityid);
            return Json(res);
        }
       







    }
}
