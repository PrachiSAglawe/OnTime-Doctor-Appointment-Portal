using Core;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Web.CuthAuth;

namespace Web.Areas.ClinicArea.Controllers
{
    [Area("ClinicArea")]
    [ClinicAuth]
    public class ClinicHomeController : Controller
    {
        IClinic repo;
        ICity Crepo;
        IDoctor doctor;
        IPatient patient;
        public ClinicHomeController(IClinic repo,ICity Crepo, IDoctor doctor,IPatient patient)
        {
            this.repo = repo;
            this.Crepo = Crepo;
            this.doctor = doctor;
            this.patient = patient;
        }
        public async Task<IActionResult> Index()
        {
            var res=await this.repo.GetAll();
            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Cities = new SelectList(await this.Crepo.GetAll(), "CityID", "CityName");
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(Clinic rec)
        {
            ViewBag.Cities = new SelectList(await this.Crepo.GetAll(), "CityID", "CityName");
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
        [HttpGet]
        public async Task<IActionResult> Edit(Int64 id)
        {
            var res = await this.repo.GetByID(id);
            ViewBag.Cities = new SelectList(await this.Crepo.GetAll(), "CityID", "CityName");
            return View(res);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Clinic rec)
        {
            ViewBag.Cities = new SelectList(await this.Crepo.GetAll(), "CityID", "CityName");
            if (ModelState.IsValid)
            {
                var res = await this.repo.Update(rec);
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
        public async Task<IActionResult> PatientReport()
        {
            ViewBag.Doctor=new SelectList(await this.doctor.GetAll(),"DoctorID","FullName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PatientReportAll(DateTime Fromdate,DateTime Todate,Int64 DID)
        {
            ViewBag.Doctor = new SelectList(await this.doctor.GetAll(), "DoctorID", "FullName");
            var res = await this.patient.GetAllPatient(Fromdate, Todate, DID);
            return View(res);
        }
        [HttpGet]
        public async Task< IActionResult> Collection()
        {
            ViewBag.Doctor = new SelectList(await this.doctor.GetAll(), "DoctorID", "FullName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetCollectionPatient(DateTime Fromdate, DateTime Todate, Int64 DID)
        {
            ViewBag.Doctor = new SelectList(await this.doctor.GetAll(), "DoctorID", "FullName");
            var res = await this.patient.GetPatientAmount(Fromdate, Todate, DID);
            return View(res);
        }
    }
}
