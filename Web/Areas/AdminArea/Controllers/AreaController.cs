using Core;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.CuthAuth;

namespace Web.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [AdminAuth]
    public class AreaController : Controller
    {
        IArea repo;
        ICity cityrepo;
           
        public AreaController(ICity cityrepo,IArea repo)
        {
            this.repo = repo;
            this.cityrepo = cityrepo;
        }
        public async Task< IActionResult> Index()
        {
            var res=await this.repo.GetAll();
            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Cities=new SelectList(await this.cityrepo.GetAll(),"CityID","CityName");
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(Area rec)
        {
            ViewBag.Cities = new SelectList(await this.cityrepo.GetAll(), "CityID", "CityName");

            if (ModelState.IsValid) 
            {
              var res = await this.repo.Insert(rec);
                TempData["Message"]=res.Message;
                if (res.IsSuccess)
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
            ViewBag.Cities = new SelectList(await this.cityrepo.GetAll(), "CityID", "CityName",res.AreaID);
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Area rec)
        {
            ViewBag.Cities = new SelectList(await this.cityrepo.GetAll(), "CityID", "CityName");

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


    