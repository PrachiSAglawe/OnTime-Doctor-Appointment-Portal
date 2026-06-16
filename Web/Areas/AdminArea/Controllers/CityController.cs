using Core;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Web.CuthAuth;

namespace Web.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [AdminAuth]
    public class CityController : Controller
    {
        ICity repo;
        IState Staterepo;

        public CityController(ICity repo, IState staterepo)
        {
            this.repo = repo;
            this.Staterepo = staterepo;
        }
        public async Task<IActionResult> Index()
        {
            var res= await this.repo.GetAll();
            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.States=new SelectList(await this.Staterepo.GetAll(),"StateID","StateName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(City rec)
        {
            ViewBag.States = new SelectList(await this.Staterepo.GetAll(), "StateID", "StateName");

            if (ModelState.IsValid) { 
                var res= await this.repo.Insert(rec);
                TempData["Message"]=res.Message;
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
            ViewBag.States = new SelectList(await this.Staterepo.GetAll(),"StateID","StateName",res.StateID);
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(City rec)
        {
            ViewBag.States = new SelectList(await this.Staterepo.GetAll(),"StateID","StateName");
            if (ModelState.IsValid)
            {
                var res = await this.repo.Update(rec);
                TempData["Message"]= res.Message;
                if (res.IsSuccess) { 
                
                  return RedirectToAction("Index");
                
                }
            }
            return View(rec);
          
        }

        public async Task<IActionResult> Delete(Int64 id)
        {
            var res = await this.repo.Delete(id);
            TempData["Message"]= res.Message;
            return RedirectToAction("Index");
        }

    }
}
