using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.ViewComponents
{
    public class PrescribeVC:ViewComponent
    {
        IPrescribtion prescrib;
        public PrescribeVC(IPrescribtion prescrib)
        {
            this.prescrib = prescrib;
        }
        public async Task<IViewComponentResult>InvokeAsync(Int64 Doctorid,Int64 sessionid)
        {
            var result = await this.prescrib.GetPriscribed(Doctorid,sessionid);
            return View(result);
        }
    }
}
