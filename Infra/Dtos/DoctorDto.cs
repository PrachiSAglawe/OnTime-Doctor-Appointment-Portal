using Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Dtos
{
   public class DoctorDto
    {
        public Int64 DoctorID { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string DoctorExperience { get; set; }
        [Required]
        public Int64 AreaID { get; set; }
        [Required]
        public virtual Area Area { get; set; }
     
        public string PhotoPath { get; set; }
      
        public decimal OPDFees {  get; set; }
        public IFormFile PhotoFile { get; set; }
        [Required]
        public string DoctorQualification { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public Int64 CountryID { get; set; }
        public Int64 StateID {  get; set; }
        [Required]
        public Int64 CityID {  get; set; }
        public Int64 ClinicID {  get; set; }
    }
}
