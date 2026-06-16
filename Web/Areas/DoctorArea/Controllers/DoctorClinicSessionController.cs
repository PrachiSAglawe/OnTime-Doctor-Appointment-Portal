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
    public class DoctorClinicSessionController : Controller
    {
        IDoctorClinicSession repo;
        IDoctor doctor;
        IClinic clinic;
        IOPDSession session;

        public DoctorClinicSessionController(IDoctorClinicSession repo, IDoctor doctor, IClinic clinic, IOPDSession session)
        {
            this.repo = repo;
            this.doctor = doctor;
            this.clinic = clinic;
            this.session = session;
        }
        public async Task<IActionResult> Index()
        {
            var res= await this.repo.GetAll();
            return View(res);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.doctors= new SelectList(await  this.doctor.GetAll(),"DoctorID","FullName");
            ViewBag.clinics = new SelectList(await this.clinic.GetAll(), "ClinicID", "ClinicName");
            ViewBag.sessions=new SelectList(await this.session.GetAll(),"OPDSessionID","SessionName");
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(DoctorClinicSession rec)
        {
            ViewBag.doctors = new SelectList(await this.doctor.GetAll(), "DoctorID", "FullName");
            ViewBag.clinics = new SelectList(await this.clinic.GetAll(), "ClinicID", "ClinicName");
            ViewBag.sessions = new SelectList(await this.session.GetAll(), "OPDSessionID", "SessionName");
            
            if (ModelState.IsValid)
            {
                var res = await this.repo.Insert(rec);
               
                TempData["Message"]=res.Message;
                if(res.IsSuccess)
                {
                    return RedirectToAction("Index");
                }

            }
            return View();  
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Int64 id)
        {
            var res = await this.repo.GetByID(id);
            ViewBag.doctors = new SelectList(await this.doctor.GetAll(), "DoctorID", "FullName",res.DoctorID);
            ViewBag.clinics = new SelectList(await this.clinic.GetAll(), "ClinicID", "ClinicName",res.ClinicID);
            ViewBag.sessions = new SelectList(await this.session.GetAll(), "OPDSessionID", "SessionName",res.OPDSessionID);
            
            return View(res);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DoctorClinicSession rec)
        {
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
            var res =await this.repo.Delete(id);
            TempData["Message"] = res.Message;
            return RedirectToAction("Index");
        }

        [HttpGet]

        public async Task<IActionResult> GetclinicJson(Int64 doctorid)
        {
            var rec = await this.doctor.GetClinicByID(doctorid);
            return Json(rec);
        }
    }
}
