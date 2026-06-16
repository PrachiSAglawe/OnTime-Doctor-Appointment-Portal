using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Infra.Dtos
{
    public class PaymentHistoryDto
    {
        public string DoctorName {  get; set; }
        public string PatientName {  get; set; }
        public decimal Amount {  get; set; }
        public string PaymentDate {  get; set; }
        public PaymentModeEnum PaymentMode { get; set; }
    }
}
