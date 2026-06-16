using Core;
using Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Classes
{
    public class CountryRepo:GenericRepo<Country>,ICountry
    {
        HospitalContext _hospitalcontext;

        public CountryRepo(HospitalContext hospitalcontext):base(hospitalcontext)
        {
            this._hospitalcontext = hospitalcontext;
        }
    }
}
