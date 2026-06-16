using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Core
{
    public class Patient
    {
        [Key]
        public Int64 PatientID {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string Address {  get; set; }
        public string MobileNo {  get; set; }
        public GenderEnum Gender {  get; set; }
        public string EmailID {  get; set; }
        public string PhotoPath {  get; set; }
        [NotMapped]
        public IFormFile PhotoFile {  get; set; }
        public Int64 UserID {  get; set; }
        public virtual User User { get; set; }

        public virtual List<BookedAppointments> BookedAppointments { get; set; }
        public Patient()
        {
            this.BookedAppointments = new List<BookedAppointments>();
        }
    }
}
