using Core;
using Infra.Dtos;
using Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Infra.Repositories.Classes
{
    public class PrescribtionRepo:GenericRepo<Prescription>,IPrescribtion
    {
        HospitalContext hospitalContext;
        public PrescribtionRepo(HospitalContext hospitalContext) : base(hospitalContext) { 
       
            this.hospitalContext = hospitalContext;
        
        }

        public Task<List<BookedAppointments>>GetPriscribed(long doctorid, long sessionid)
        {
            var res = from t in this.hospitalContext.BookedAppointments
                      where t.Prescriptions.Any(p => p.BookedAppointmentsID == t.BookedAppointmentsID)
                      && t.AppointmentDate.Date == DateTime.Today
                      join t1 in this.hospitalContext.DoctorClinicSession
                       on t.DoctorClinicSessionID equals t1.DoctorClinicSessionID
                      where t1.DoctorID == doctorid && t1.OPDSessionID == sessionid
                      select t;
            return res.ToListAsync();


        }

        public async Task<RepoResultDto> GetPrescribedFormInfo(PrescribeInfoDto rec, long[] MedicineID, string[] Frequency, string[] Dosage, string[] Qty, UnitEnum[] Unit)
        {
            RepoResultDto result = new RepoResultDto();
            try
            {

                Prescription pres = new Prescription();
                pres.BookedAppointmentsID = rec.BookedAppointmentID;
                pres.PrescriptionDate = rec.PrescriptionDate;
                pres.DoctorID = rec.DoctorID;


                for (int i = 0; i < Dosage.Length; i++)
                {
                    PrescriptionMedicine pmed = new PrescriptionMedicine();
                    pmed.Frequency = Frequency[i];
                    pmed.Dosage = Dosage[i];
                    pmed.Qty = Qty[i];
                    pmed.Unit = Unit[i];
                    pmed.MedicineID = MedicineID[i];
                    pres.PrescriptionMedicines.Add(pmed);

                }

                PrescriptionQuality PresQuality = new PrescriptionQuality();
                PresQuality.NextVisit = rec.nextVisit;
                PresQuality.Suggestion = rec.Suggestions;

                pres.PrescriptionQuality.Add(PresQuality);

                BookedAppPayment payment = new BookedAppPayment()
                {
                    BookedAppointmentsID = rec.BookedAppointmentID,
                    Amount = rec.Amount,
                    PaymentMode = rec.PaymentMode,
                };

                this.hospitalContext.BookedAppPayment.Add(payment);

                await this.hospitalContext.Prescription.AddAsync(pres);
                await hospitalContext.SaveChangesAsync();

                result.IsSuccess = true;
                result.Message = "Prescribtion Create Successfully";
            }
            catch (Exception err)
            {
                result.IsSuccess = false;
                result.Message = err.Message;
            }
            return result;
        }
        public async Task<Prescription>GetByID(long PID)
        {
            return await this.hospitalContext.Prescription.FindAsync(PID);
        }



        public async Task<decimal> GetOPDFees(long bookapootid)
        {
            var fees = await (from t in this.hospitalContext.BookedAppointments
                              join t1 in this.hospitalContext.DoctorClinicSession
                              on t.DoctorClinicSessionID equals t1.DoctorClinicSessionID
                              where t.BookedAppointmentsID == bookapootid
                              join t2 in this.hospitalContext.doctors
                              on t1.DoctorID equals t2.DoctorID
                              select t2.OPDFees).FirstOrDefaultAsync();
            return fees;

        }
        public async Task<long> GetPrescribtion()
        {
            Int64 PID = await this.hospitalContext.Prescription.OrderByDescending(t => t.PrescriptionID).Select(p => p.PrescriptionID).FirstOrDefaultAsync();
            return PID;
        }

        

            public async Task<PrescribeInfoDto> GetPresByID(long bookAppId)
            {
                 var res = await (from t in this.hospitalContext.BookedAppointments
                     join t1 in this.hospitalContext.Prescription
                         on t.BookedAppointmentsID equals t1.BookedAppointmentsID
                     where t1.BookedAppointmentsID == bookAppId
                     join t3 in this.hospitalContext.PrescriptionQuality
                         on t1.PrescriptionID equals t3.PrescriptionID
                     join t4 in this.hospitalContext.BookedAppPayment
                         on t.BookedAppointmentsID equals t4.BookedAppointmentsID
                     select new PrescribeInfoDto
                     {
                         PrescriptionID = t1.PrescriptionID,
                         nextVisit = t3.NextVisit,
                         Suggestions = t3.Suggestion,
                         PaymentMode = t4.PaymentMode,
                         Amount = t4.Amount,

                         MedicineID = this.hospitalContext.PrescriptionMedicine
                                        .Where(m => m.PrescriptionID == t1.PrescriptionID)
                                        .Select(m => m.MedicineID)
                                        .ToList(),

                         MedicineTypes = this.hospitalContext.PrescriptionMedicine
                                        .Where(m => m.PrescriptionID == t1.PrescriptionID)
                                        .Select(m => (long)m.Medicine.MedicineTypes) 
                                        .ToList(),

                         Frequency = this.hospitalContext.PrescriptionMedicine
                                        .Where(m => m.PrescriptionID == t1.PrescriptionID)
                                        .Select(m => m.Frequency)
                                        .ToList(),

                         Dosage = this.hospitalContext.PrescriptionMedicine
                                        .Where(m => m.PrescriptionID == t1.PrescriptionID)
                                        .Select(m => m.Dosage)
                                        .ToList(),

                         Qty = this.hospitalContext.PrescriptionMedicine
                                        .Where(m => m.PrescriptionID == t1.PrescriptionID)
                                        .Select(m => m.Qty)
                                        .ToList(),

                         Unit = this.hospitalContext.PrescriptionMedicine
                                        .Where(m => m.PrescriptionID == t1.PrescriptionID)
                                        .Select(m => m.Unit)
                                        .ToList(),

                         PrescriptionMedicines = this.hospitalContext.PrescriptionMedicine
                                        .Where(m => m.PrescriptionID == t1.PrescriptionID)
                                        .ToList()
                     }).FirstOrDefaultAsync();

    return res;
}

       
        public async Task<RepoResultDto> EditPresforMultiple(PrescribeInfoDto rec, long[] MedicineID, string[] Frequency, string[] Dosage, string[] Qty, UnitEnum[] Unit)
        {
            RepoResultDto result = new RepoResultDto();
            try
            {
                //Find nd Delete old multile record:it used only when you want to delete multiple record
                var PresMedi = this.hospitalContext.PrescriptionMedicine.Where(p => p.PrescriptionID == rec.PrescriptionID);
                foreach (var temp in PresMedi)
                {
                    this.hospitalContext.PrescriptionMedicine.RemoveRange(temp);
                }


                
                var pres = await this.hospitalContext.Prescription.FindAsync(rec.PrescriptionID);
                pres.PrescriptionID = rec.PrescriptionID;
                pres.BookedAppointmentsID = rec.BookedAppointmentID;
                pres.PrescriptionDate = rec.PrescriptionDate;
                pres.DoctorID = rec.DoctorID;

                //insert new data
                for (var i = 0; i < rec.Qty.Count; i++)
                {
                    PrescriptionMedicine med = new PrescriptionMedicine();
                    med.Frequency = rec.Frequency[i];
                    med.Qty = rec.Qty[i];
                    med.Dosage = rec.Dosage[i];
                    med.Unit = rec.Unit[i];
                    med.MedicineID = rec.MedicineID[i];                   
                    pres.PrescriptionMedicines.Add(med);
                }


                var oldpresQuality = await this.hospitalContext.PrescriptionQuality
               .FirstOrDefaultAsync(p => p.PrescriptionID == rec.PrescriptionID);

                oldpresQuality.NextVisit= rec.nextVisit;
                oldpresQuality.Suggestion = rec.Suggestions;

                pres.PrescriptionQuality.Add(oldpresQuality);

                var Pay = await this.hospitalContext.BookedAppPayment.FirstOrDefaultAsync(p=>p.BookedAppointmentsID==rec.BookedAppointmentID);
                Pay.BookedAppointmentsID = rec.BookedAppointmentID;
                Pay.Amount = rec.Amount;
                Pay.PaymentMode = rec.PaymentMode;
                this.hospitalContext.BookedAppPayment.Update(Pay);

                 this.hospitalContext.Prescription.Update(pres);

                await this.hospitalContext.SaveChangesAsync();
                result.Message = "Update sucessfully";
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.IsSuccess = false;
            }
            return result;
        }
    }
    }

