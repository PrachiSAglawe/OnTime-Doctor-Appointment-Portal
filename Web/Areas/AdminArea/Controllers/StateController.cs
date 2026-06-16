using Core;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.CuthAuth;

namespace Web.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [AdminAuth]
    public class StateController : Controller
    {
        IState repo;
        ICountry country;

        public StateController(IState repo,ICountry country)
        {
            this.repo = repo;
            this.country = country;
        }
        public async Task<IActionResult> Index()
        {
            var res= await this.repo.GetAll();
            return View(res);
        }
        [HttpGet]
        public async Task< IActionResult> Create()
        {
            ViewBag.Countries = new SelectList(await this.country.GetAll(), "CountryID", "CountryName");
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(State rec)
        {
            ViewBag.Countries = new SelectList(await this.country.GetAll(), "CountryID", "CountryName");
            if (ModelState.IsValid)
            {
            
             var res= await this.repo.Insert(rec);
               TempData["Message"]=res.Message;
                if (res.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            
            }
            return View(rec) ;
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Int64 id)
        {
            var res = await this.repo.GetByID(id);
            ViewBag.Countries = new SelectList(await this.country.GetAll(), "CountryID", "CountryName",res.StateID);
            return View(res);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(State rec)
        {
            ViewBag.Countries = new SelectList(await this.country.GetAll(), "CountryID", "CountryName");
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
            var res=await this.repo.Delete(id);
            TempData["Message"]= res.Message;
            return RedirectToAction("Index");
        }

    }
}
