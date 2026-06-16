using Core;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.CuthAuth;

namespace Web.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [AdminAuth]
    public class CountryController : Controller
    {
        ICountry repo;
        public CountryController(ICountry repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var res=await this.repo.GetAll();
            return View(res);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Country rec)
        {
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
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Country rec)
        {
            if (ModelState.IsValid)
            {
                var res = await this.repo.Update(rec);
                TempData["Message"] = res.Message;
                if (res.IsSuccess)
                {
                    return RedirectToAction("index");
                }
            }
            return View(rec);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Int64 id)
        {
          var res=await this.repo.Delete(id);
            TempData["Message"] = res.Message;
            return RedirectToAction("index");


        
        }
    }
}
