using Core;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.CuthAuth;

namespace Web.Areas.ClinicArea.Controllers
{
    [Area("ClinicArea")]
    [ClinicAuth]
    public class ClinicCertificateHomeController : Controller
    {


        IClinicCertificate repo;
        IClinic Crepo;
       public ClinicCertificateHomeController(IClinicCertificate repo, IClinic crepo)
        {
            this.repo = repo;
            this.Crepo = crepo;
        }
        

        public async Task<IActionResult> Index()
        {
            var res = await this.repo.GetAll();
            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Clinic = new SelectList(await this.Crepo.GetAll(), "ClinicID", "ClinicName");
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(ClinicCertificate rec)
        {
            ViewBag.Clinic = new SelectList(await this.Crepo.GetAll(), "ClinicID", "ClinicName");
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
            ViewBag.Clinic = new SelectList(await this.Crepo.GetAll(), "ClinicID", "ClinicName");
            return View(res);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(ClinicCertificate rec)
        {
            ViewBag.Clinic = new SelectList(await this.Crepo.GetAll(), "ClinicID", "ClinicName");
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
    }
}
