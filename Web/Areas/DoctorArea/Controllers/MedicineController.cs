using Core;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Web.Areas.DoctorArea.Controllers
{
    [Area("DoctorArea")]
    public class MedicineController : Controller
    {
        IMedicine medicine;
        public MedicineController(IMedicine medicine)
        {
            this.medicine = medicine;
        }
        public async Task<IActionResult> Index()
        { 
            var res= await this.medicine.GetAll();
            return View(res);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        public async Task<IActionResult> Create(Medicine rec)
        {
           
            if (ModelState.IsValid)
            {
                var res = await this.medicine.Insert(rec);
                TempData["Message"] = res.Message;
                if (res.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(rec);
        }
    }
}
