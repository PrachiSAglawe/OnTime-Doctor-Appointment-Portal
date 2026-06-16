using Core;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.CuthAuth;

namespace Web.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [AdminAuth]
    public class SpecilityController : Controller
    {
        ISpecility repo;
        public SpecilityController(ISpecility repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var res = await this.repo.GetAll();
            return View(res);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Specility rec)
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
        public async Task<IActionResult>Edit(Int64 id)
        {
            var res= await this.repo.GetByID(id);
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Specility rec)
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

        public async Task<IActionResult> Delete(Int64 id) { 
           var res=await this.repo.Delete(id);
            TempData["Message"]=res.Message;
            return RedirectToAction("Index");   
        
        }
    }
}
