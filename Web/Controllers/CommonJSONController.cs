using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class CommonJSONController : Controller
    {
        ICountry country;
        IState state;
        ICity city;
        IArea area;
    
        public CommonJSONController(ICountry country,IState state, IArea area, ICity city)
        {
            this.country = country;
            this.area = area;
            this.state = state;
            this.city = city;
            
        }
        [HttpGet]

        public async Task<IActionResult> GetstateJson(Int64 CountryID)
        {
            var rec = await this.state.GetStatebyID(CountryID);
            return Json(rec);
        }
        [HttpGet]
        public async Task<IActionResult> GetCityJson(Int64 stateid)
        {
            var res = await this.city.GetCityByStateID(stateid);
            return Json(res);
        }
        [HttpGet]

        public async Task<IActionResult> GetAreaJSON(Int64 cityid)
        {
            var res = await this.area.GetAreaByCityID(cityid);
            return Json(res);
        }

      


    }
}
