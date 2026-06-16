using Core;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceStack.Text;
using System.Threading.Tasks;
using Web.CuthAuth;

namespace Web.Areas.UserArea.Controllers
{
    [Area("UserArea")]
    [UserAuth]
    public class UserHomeController : Controller
    {
        IUser repo;
        IWebHostEnvironment env;
        IPatient patient;
        IClinic clinic;
        IDoctor doctor;
        public UserHomeController(IUser repo, IWebHostEnvironment env, IPatient patient,IClinic clinic,IDoctor doctor)
        {
            this.repo = repo;
            this.env = env;
            this.patient = patient;
            this.clinic = clinic;
            this.doctor = doctor;
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
                Int64 id = Convert.ToInt64(HttpContext.Session.GetString("UserID"));
                var res = await this.repo.ChangePassword(rec, id);
                ModelState.AddModelError("", res.Message);
                return View();
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            Int64 userid = Convert.ToInt64(HttpContext.Session.GetString("UserID"));
            var rec = await this.repo.GetUserByID(userid);

            return View(rec);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(User rec)
        {
            Int64 userid = Convert.ToInt64(HttpContext.Session.GetString("UserID"));
            var res = await this.repo.EditProfile(rec, userid);
            ModelState.AddModelError("", res.Message);
            return View(rec);
        }

        public IActionResult BookAppPrescribe()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult>PrintPrescription(Int64 id,Int64 bookappoID)
        {
            HttpContext.Session.SetString("PatientID", id.ToString());
           
            var res = await this.repo.GetPrescribtionID(bookappoID);
            return View(res);
        }

        public async Task<IActionResult> AllPatients()
        {
            Int64 userid = Convert.ToInt64(HttpContext.Session.GetString("UserID"));
            var res = await this.repo.GetPatient(userid);
            return View(res.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Users = new SelectList(await this.repo.GetAll(), "UserID", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Patient rec)
        {
            ViewBag.Users = new SelectList(await this.repo.GetAll(), "UserID", "FullName");

            if (ModelState.IsValid)
            {
                if (rec.PhotoFile != null)
                {
                    if (rec.PhotoFile.Length > 0)
                    {
                        //logical path
                        string logpath = "PatientsPhotos";

                        string folderpath = Path.Combine(env.WebRootPath, logpath);
                        string filepath = Path.Combine(folderpath, rec.PhotoFile.FileName);

                        FileStream fs = new FileStream(filepath, FileMode.Create);
                        await rec.PhotoFile.CopyToAsync(fs);
                        fs.Close();
                        rec.PhotoPath = "/PatientsPhotos/" + rec.PhotoFile.FileName;
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


                var res = await this.patient.Insert(rec);
                TempData["Message"] = res.Message;
                if (res.IsSuccess)
                {
                    return RedirectToAction("AllPatients");
                }
            }

            return View(rec);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Int64 id)
        {

            var res = await this.patient.GetByID(id);
            ViewBag.Users = new SelectList(await this.repo.GetAll(), "UserID", "FullName",res.UserID);


            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Patient rec)
        {

            ViewBag.Users = new SelectList(await this.repo.GetAll(), "UserID", "FullName");

            if (ModelState.IsValid)
            {
                if (rec.PhotoFile != null)
                {
                    if (rec.PhotoFile.Length > 0)
                    {
                        //logical path
                        string logpath = "PatientsPhotos";

                        string folderpath = Path.Combine(env.WebRootPath, logpath);
                        string filepath = Path.Combine(folderpath, rec.PhotoFile.FileName);

                        FileStream fs = new FileStream(filepath, FileMode.Create);
                        await rec.PhotoFile.CopyToAsync(fs);
                        fs.Close();
                        rec.PhotoPath = "/PatientsPhotos/" + rec.PhotoFile.FileName;
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


                var res = await this.patient.Update(rec);
                TempData["Message"] = res.Message;
                if (res.IsSuccess)
                {
                    return RedirectToAction("AllPatients");
                }
            }

            return View(rec);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Int64 id)
        {
            var res = await this.patient.Delete(id);
            TempData["Message"] = res.Message;
            return RedirectToAction("AllPatients");
        }
        [HttpGet]
        public async Task<IActionResult> Details(Int64 id)
        {
            var res = await this.patient.GetByID(id);
            return View(res);
        }
        public async Task<IActionResult> PaymentHistory()
        {
          Int64 id = Convert.ToInt64(HttpContext.Session.GetString("UserID"));
            var res = await this.repo.GetPaymentHistory(id);

            return View(res);
        }

        public async Task<IActionResult> CreateNewAppoint()
        {
            ViewBag.ClinicName = new SelectList(await this.clinic.GetAll(), "ClinicID","ClinicName");
            ViewBag.Doctors = new SelectList(await this.doctor.GetAll(), "DoctorID","FullName" );
            ViewBag.Patients = new SelectList(await this.patient.GetAll(), "PatientID","FullName");
            return View();
        }

    }
}
