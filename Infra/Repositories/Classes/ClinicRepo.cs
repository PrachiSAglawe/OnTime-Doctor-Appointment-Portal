using Core;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Classes
{
    public class ClinicRepo:GenericRepo<Clinic>,IClinic
    {
        HospitalContext hospitalContext;
        public ClinicRepo(HospitalContext hospitalContext):base(hospitalContext)
        {
          this.hospitalContext = hospitalContext;
        
        }

     
    }
}
