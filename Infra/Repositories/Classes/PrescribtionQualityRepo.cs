using Core;
using Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Classes
{
    public class PrescriptionQualityRepo:GenericRepo<PrescriptionQuality>,IPrescribtionQuality
    {
        HospitalContext hospitalContext;
        public PrescriptionQualityRepo(HospitalContext hospitalContext) : base(hospitalContext) { }
    }
}
