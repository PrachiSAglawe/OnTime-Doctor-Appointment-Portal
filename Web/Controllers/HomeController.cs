using Core;
using Infra;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceStack;
using ServiceStack.Text;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
       
        IDoctor doctor;
        IDoctorCalender dc;
        IDoctorClinicSession ds;
        IUser urepo;
        IWebHostEnvironment env;
        IBookAppoinment bookapp;
        HospitalContext hospitalContext;
       
       public HomeController(IDoctor doctor,IDoctorCalender dc,IDoctorClinicSession ds,IUser urepo,IWebHostEnvironment env,IBookAppoinment bookapp,HospitalContext hospitalContext)
        {
            this.doctor = doctor;
            this.dc = dc;
            this.urepo = urepo;
            this.ds = ds;
            this.env = env;
            this.bookapp= bookapp;
            this.hospitalContext = hospitalContext;
        }

        public IActionResult Index()
        {

            return View();
        }
        public async Task<IActionResult> SearchdoctorInfo(Int64 SpecilityID=0, Int64 CityID=0,Int64 AreaID=0)
        {

            if (SpecilityID > 0 &&  AreaID > 0)
            {
                var res = await this.doctor.SearchDoctorAreaSpe( AreaID, SpecilityID);
                return View(res.ToList());
            }
            else if (SpecilityID>0 && CityID>0) {
                var res = await this.doctor.SearchDoctor(SpecilityID, CityID);
                return View(res.ToList());
            }
            else
            {
                return View(await this.doctor.GetAllDoctor());
               
            }
        }

        [HttpPost]
        public async Task<IActionResult> DoctorInfoCalender(Int64 id)
        {
           ViewBag.SessionName = new SelectList(await this.dc.GetSessionByDoctorID(id), "DoctorClinicSessionID", "SessionName");

         
            var availableDates = await this.dc.GetAvailableDates(id);
           
            ViewBag.AvailableDates = availableDates;           
            ViewBag.DoctorID = id;
            return View();
        }
       

        [HttpGet]
        public IActionResult GetTimeSlots(Int64 sessionId, DateTime appointmentDate)
        {
                         
            var allSlots = dc.GetTimeSlots(sessionId).ToList();

       
            var bookedSlots = hospitalContext.BookedAppointments
                .Where(a => a.DoctorClinicSessionID == sessionId
                         && a.AppointmentDate.Date == appointmentDate.Date)
                .Select(a => a.StartTime) 
                .ToList();

            return Json(new { allSlots, bookedSlots });

        }

        [HttpGet]

        public async Task<IActionResult> BookAppoinment( DateTime AppointmentDate, string StartTime, Int64 DoctorClinicSessionID)
        
        {
            
            
            ViewBag.Users = new SelectList(await this.urepo.GetAll(), "UserID", "FullName");


            var TimeInterval = await this.bookapp.FindTimeInterval(DoctorClinicSessionID);

            DateTime startTime = DateTime.ParseExact(StartTime, "h:mm tt", CultureInfo.InvariantCulture);
            DateTime endTime = startTime.AddMinutes(TimeInterval);

            ViewBag.EndTime = endTime.ToString("h:mm tt");


            ViewBag.AppointentTime = AppointmentDate.ToString("yyyy-MM-dd");
            ViewBag.DoctorClinicSessionID= DoctorClinicSessionID;
            ViewBag.StartTime = StartTime;



            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BookAppoinment(BookAppointmentDto rec)
        {

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


                var res = await this.ds.BookAppointmentPatient(rec);

                TempData["Message"] = res.Message;
                if (res.IsSuccess)
                {
                    return RedirectToAction("BookMessage");
                   

                }
            }
            return View(rec);
        }

        public IActionResult BookMessage()
        {
            
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

      


    }
}
