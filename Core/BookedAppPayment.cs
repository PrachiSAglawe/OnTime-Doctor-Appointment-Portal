using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Core
{
    public class BookedAppPayment
    {
        public Int64 BookedAppPaymentID {  get; set; }
        public Int64 BookedAppointmentsID {  get; set; }
        public virtual BookedAppointments BookedAppointments { get; set; }
        public decimal Amount {  get; set; }
        public PaymentModeEnum PaymentMode {  get; set; }

    }
}
