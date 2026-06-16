using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.ViewComponents
{
    public class PatientInfoVC:ViewComponent
    {
        IPatient patient;
        public PatientInfoVC(IPatient patient)
        {
            this.patient = patient;
        }
          
        public async Task<IViewComponentResult> InvokeAsync(Int64 patientid)
        {
            var res = await this.patient.GetPatientInfo(patientid);
            return View(res);
        }
    }
}
