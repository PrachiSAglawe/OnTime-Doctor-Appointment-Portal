using Core;
using Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Classes
{
    public class ClinicCertificateRepo:GenericRepo<ClinicCertificate>,IClinicCertificate
    {
        HospitalContext hospitalContext;
        public ClinicCertificateRepo(HospitalContext hospitalContext) : base(hospitalContext)
        {
            this.hospitalContext = hospitalContext;
        }
    }
}
