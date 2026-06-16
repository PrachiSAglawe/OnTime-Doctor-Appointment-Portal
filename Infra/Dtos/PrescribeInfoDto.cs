using Core;
using Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Infra.Dtos
{
    public class PrescribeInfoDto
    {
        public Int64 PrescribtionID { get; set; }
        //public Int64 PrescriptionMedicineID { get; set; }
        //public Int64 PrescriptionQualityID { get; set; }
       // public Int64 MedicineID { get; set; }
        //public MedicineTypes MedicineTypes { get; set; }
        //public string Frequency {  get; set; }
        //public string Dosage {  get; set; }
        //public string Qty {  get; set; }
        //public UnitEnum Unit { get; set; }
        public string nextVisit {  get; set; }
        public string Suggestions {  get; set; }
        public string OPDFees {  get; set; }
        public PaymentModeEnum PaymentMode {  get; set; }
        public decimal Amount {  get; set; }
        public string PrescriptionDate {  get; set; }
        public Int64 PrescriptionID { get; set; }
        public Int64 DoctorID {  get; set; }
        public Int64 BookedAppointmentID { get; set; }        
        public List<string> Frequency {  get; set; }
        public List<string> Dosage {  get; set; }
        public List<string> Qty {  get; set; }
        public List<UnitEnum> Unit { get; set; }
        public List<long> MedicineID { get; set; } = new List<long>();
        public List<long> MedicineTypes { get; set; } = new List<long>();
        public virtual List<PrescriptionMedicine> PrescriptionMedicines { get; set; }=new List<PrescriptionMedicine>();
        public PrescribeInfoDto()
        {
            
          
            this.Frequency = new List<string>();
            this.Qty = new List<string>();
            this.Unit = new List<UnitEnum>();
            this.Dosage = new List<string>();
        }




    }
   
}
