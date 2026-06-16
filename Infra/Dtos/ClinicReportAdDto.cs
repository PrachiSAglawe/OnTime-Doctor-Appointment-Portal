using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Dtos
{
    public class ClinicReportAdDto
    {
        public string ClinicName {  get; set; }
        public int TotalAppointment {  get; set; }
        public decimal TotalAmount {  get; set; }
    }
}
