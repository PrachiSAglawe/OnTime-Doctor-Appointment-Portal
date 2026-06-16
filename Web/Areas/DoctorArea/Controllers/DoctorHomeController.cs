using Core;
using Infra;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceStack;
using ServiceStack.Text;
using System.Threading.Tasks;
using Utilities;
using Web.CuthAuth;

namespace Web.Areas.DoctorArea.Controllers
{
    [Area("DoctorArea")]
    [DoctorAuth]
    public class DoctorHomeController : Controller
    {
        IDoctor repo;
        IWebHostEnvironment env;
        IArea area;
        IMedicine medicines;
        IBookAppoinment bookapp;
        IPrescribtion prescription;
        IMedicine medicine;
        public DoctorHomeController(IDoctor repo,IWebHostEnvironment env,IArea area,IMedicine medicines,IBookAppoinment bookapp,IPrescribtion prescription,IMedicine medicine)
        {
            this.repo = repo;
            this.env = env;
            this.area = area;
            this.medicines = medicines;
            this.bookapp = bookapp;
            this.prescription = prescription;
            this.medicine = medicine;
        }
        

        

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {

            Int64 DID = Convert.ToInt64(HttpContext.Session.GetString("DoctorID"));
            var res= await this.repo.GetDoctorByIDEdit(DID);
            ViewBag.Areas = new SelectList(await this.area.GetAll(), "AreaID", "AreaName",res.AreaID);
            return View(res);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(Doctor rec)
        {
            Int64 DID = Convert.ToInt64(HttpContext.Session.GetString("DoctorID"));
            ViewBag.Areas = new SelectList(await this.area.GetAll(), "AreaID", "AreaName");
             var res= await this.repo.EditProfile(rec,DID);
            ModelState.AddModelError("" ,res.Message);
            return View(rec);
            

            
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto rec)
        {
            if (ModelState.IsValid) {
                Int64 id = Convert.ToInt64(HttpContext.Session.GetString("DoctorID"));
                var res = await this.repo.ChangePassword(rec, id);
                ModelState.AddModelError("", res.Message);
                return View();


            }
            return View();
          

        }
        [HttpGet]
        public async Task<IActionResult> Prescribed(Int64 id,Int64 bookapootid)
        {
            ViewBag.BookAppointmentID = bookapootid;
          
            ViewBag.MedicineName = new SelectList(await this.medicines.GetAll(), "MedicineID", "MedicineName");
            HttpContext.Session.SetString("PatientID", id.ToString());
            var opdfees = await this.prescription.GetOPDFees(bookapootid);
            ViewBag.OPDFees = opdfees;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Prescribed(PrescribeInfoDto rec,Int64 [] MedicineID, string[] Frequency, string[] Dosage, string[] Qty, UnitEnum[] Unit)
        {           
             var res = await this.prescription.GetPrescribedFormInfo(rec, MedicineID, Frequency, Dosage, Qty, Unit);
             Int64 id = await this.prescription.GetPrescribtion();

            if (res.IsSuccess)
              {
                TempData["Message"] = res.Message;
                return RedirectToAction("Print", new { id = id });
              }
            return View(rec);



        }
        [HttpGet]
        public async Task<IActionResult> PrescribedEdit(Int64 bookapootid)
        {
            var res = await this.prescription.GetPresByID(bookapootid);
            ViewBag.BookAppointmentID = bookapootid;
            var opdfees = await this.prescription.GetOPDFees(bookapootid);
            ViewBag.OPDFees = opdfees;

            ViewBag.MedicineName = new SelectList(await this.medicines.GetAll(), "MedicineID", "MedicineName");
   
            return View(res);
        }



        [HttpPost]
        public async Task<IActionResult> PrescribedEdit(PrescribeInfoDto rec, Int64[] MedicineID, string[] Frequency, string[] Dosage, string[] Qty, UnitEnum[] Unit) 
         {
            var res = await this.prescription.EditPresforMultiple(rec,MedicineID, Frequency, Dosage, Qty, Unit);
            ViewBag.MedicineName = new SelectList(await this.medicines.GetAll(), "MedicineID", "MedicineName");
            if (res.IsSuccess)
            {
                TempData["Message"] = res.Message;
                return RedirectToAction("index");
            }

            return View(res);
         }


        [HttpGet]
        public async Task<IActionResult> Print(Int64 id)
        {
            var rec = await this.prescription.GetByID(id);
            return View(rec);
        }
        [HttpGet]
        public async Task<IActionResult> GetMedicineByType(Int64 MID)
        {
            var res = await this.medicine.GetMedicine(MID);
            return Json(res);
        }

        [HttpGet]
        public IActionResult DailyCollection()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DailyAppointCollect(DateTime date)
        {
            Int64 DID = Convert.ToInt64(HttpContext.Session.GetString("DoctorID"));
            var res = await this.bookapp.DailyColle(date,DID);
            return View(res);

        }

        [HttpGet]
        public IActionResult MissedAppoint()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetMissedAppoint(DateTime Fromdate,DateTime ToDate)
        {
            Int64 DID = Convert.ToInt64(HttpContext.Session.GetString("DoctorID"));
            ViewBag.FromDate=Fromdate.Date;
            ViewBag.ToDate=ToDate.Date;
            var res = await this.bookapp.GetAppo(Fromdate,ToDate,DID);  
            return View(res);
        }



    }
}
