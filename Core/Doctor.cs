using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
   public class Doctor
    {
        [Key]
        public Int64 DoctorID {  get; set; }
        public string FirstName {  get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName +" "+ LastName;
            }
        }
        public string LastName { get; set; }
        public string MobileNo {  get; set; }
        public bool IsAvailable { get; set; }
        public string Address {  get; set; }
        public decimal OPDFees { get; set; }
        public string DoctorExperience {  get; set; }
        public Int64 AreaID {  get; set; }
        public virtual Area Area { get; set; }
        public string PhotoPath {  get; set; }
        [NotMapped]
        public IFormFile PhotoFile {  get; set; }
       

        public string DoctorQualification {  get; set; }
        public string Password {  get; set; }
        [ForeignKey("Clinic")]
        public Int64 ClinicID { get; set; }
        public virtual Clinic Clinic { get; set; }

        public virtual List<DoctorRating> DoctorRatings { get; set; }
        public virtual List<DoctorClinicSession> DoctorClinicSessions { get; set; }

        public virtual List<DoctorSpeciality> DoctorSpecialities { get; set; }

        public virtual List<Prescription> Prescription { get; set; }

        public Doctor()
        {
           this.DoctorSpecialities = new List<DoctorSpeciality>();
        }

    }
}
