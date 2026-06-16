using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.ViewComponents
{
   

    public class MenuVC:ViewComponent
    {
        ICountry country;
        IState state;
        ICity city;
        ISpecility specility;
        public MenuVC(ICountry country, IState state, ICity city, ISpecility specility)
        {
            this.country = country;
            this.state = state;
            this.city = city;
            this.specility = specility;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            ViewBag.countries = new SelectList(await this.country.GetAll(), "CountryID", "CountryName");
            ViewBag.state = new SelectList(await this.state.GetAll(), "StateID", "StateName");
            ViewBag.city = new SelectList(await this.city.GetAll(), "CityID", "CityName");
            ViewBag.sepcility = new SelectList(await this.specility.GetAll(), "SpecilityID", "SpecilityName");
            return View();
        }

    }
}
