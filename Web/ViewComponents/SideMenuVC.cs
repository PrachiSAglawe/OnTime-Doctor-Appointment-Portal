using Infra.Repositories.Classes;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.ViewComponents
{
	public class SideMenuVC:ViewComponent
	{
		IArea repo;
		ISpecility specility;
		public SideMenuVC(IArea repo,ISpecility specility)
		{
			this.repo = repo;
			this.specility = specility;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			ViewBag.Areas = new SelectList(await this.repo.GetAll(), "AreaID", "AreaName");
            ViewBag.sepcility = new SelectList(await specility.GetAll(), "SpecilityID", "SpecilityName");
			return View();
		}
	}
}
