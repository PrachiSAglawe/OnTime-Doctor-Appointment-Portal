using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Infra.Dtos
{
   public class BookAppointmentDto
    {
        //public Int64 BookedAppointmentsID {  get; set; }
        public DateTime AppointmentDate { get; set; }
        public string StartTime{  get; set; }
        public string EndTime { get; set; }
        public Int64 DoctorClinicSessionID {  get; set; }
        public Int64 PatientID { get; set; }
        public bool IsPaid {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Address {  get; set; }
        public string MobileNo {  get; set; }
        public GenderEnum Gender {  get; set; }
        public string EmailID {  get; set; }
        public string PhotoPath {  get; set; }
        public IFormFile PhotoFile { get; set; }
        public Int64 UserID {  get; set; }
       

    }
}
