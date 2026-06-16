using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents
{
    public class SpecilityVC:ViewComponent
    {
        ISpecility repo;


        public SpecilityVC(ISpecility repo)
        {
            this.repo = repo;
        }
        public async Task<IViewComponentResult> InvokeAsync(Int64 doctorid=0)
        {
            var res = await this.repo.GetSpecilityCheckBoxes(doctorid);
            return View(res);   
        }

    }
}
