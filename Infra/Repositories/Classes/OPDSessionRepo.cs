using Core;
using Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Classes
{
    public class OPDSessionRepo:GenericRepo<OPDSession>,IOPDSession
    {
        HospitalContext hospitalContext;
        public OPDSessionRepo(HospitalContext hospitalContext):base (hospitalContext)
            { }
    }
}
