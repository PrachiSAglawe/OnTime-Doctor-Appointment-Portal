using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Infra.Dtos
{
    public class DailyCollectionDto
    {
        public DateTime AppointmentDate { get; set; }
        public string PatientName {  get; set; }
        public decimal Amount {  get; set; }
        public PaymentModeEnum PaymentMode { get; set; }


    }
}
