using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infra
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach(var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior=DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Admin>().HasData(
                new Admin() { AdminID=1,FirstName="Sunil",LastName="Pawar",EmailID="sunil@hotmail.com",Password="abcd",MobileNo="9373750490"},
                new Admin() { AdminID = 2, FirstName = "Anil", LastName = "Sumbe", EmailID = "Anil12@gmail.com", Password = "abcd", MobileNo = "9373750490" },
                new Admin() { AdminID = 3, FirstName = "Mahesh", LastName = "Tambe", EmailID = "mahesh@gmail.com", Password = "1234", MobileNo = "9373751234" }


                );
            //modelBuilder.Entity<Clinic>().HasData(
            //    new Clinic() { ClinicID=4,ClinicName="LifeCare",Address="Pradhikaran Akurdi",MobileNo="8967564534",EmailID="lifecare@gmail.com",ContactPersonName="7890231290",LandLineNo="90785690",WebsiteUrl="http://www.lifecare.com",CityID=2}

            //    );
            modelBuilder.Entity<Country>().HasData(
      new Country()
      {
          CountryID = 1,
          CountryName = "India"
      }
  );

            modelBuilder.Entity<State>().HasData(
                new State()
                {
                    StateID = 1,
                    StateName = "Maharashtra",
                    CountryID = 1
                }
            );

            modelBuilder.Entity<City>().HasData(
                new City()
                {
                    CityID = 1,
                    CityName = "Pune",
                    StateID = 1
                }
            );

            modelBuilder.Entity<Clinic>().HasData(
                new Clinic()
                {
                    ClinicID = 1,
                    ClinicName = "LifeCare",
                    Address = "Akurdi Pune",
                    MobileNo = "9876543210",
                    EmailID = "lifecare@gmail.com",
                    ContactPersonName = "Admin",
                    LandLineNo = "020123456",
                    WebsiteUrl = "http://lifecare.com",
                    CityID = 1
                }
            );

            modelBuilder.Entity<ClinicOwner>().HasData(
                new ClinicOwner()
                {
                    ClinicOwnerID = 1,
                    FirstName = "Chaitali",
                    LastName = "Mahandule",
                    Gender = "Female",
                    EmailID = "Chaitali@gmail.com",
                    MobileNo = "9078563490",
                    Password = "abcd",
                    ClinicID = 1
                }
            );

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Citys { get; set; }
        public DbSet<Area> Areas { get; set; }

        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<OPDSession> OPDSessions { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<ClinicFacility> ClinicFacilities { get; set; }
        public DbSet<Specility> Specilities { get; set; }
        public DbSet<DoctorRating> DoctorRatings { get; set; }
        public DbSet<ClinicRating> ClinicRatings { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<BookedAppointments> BookedAppointments { get; set; }
        public DbSet<BookedAppPayment> BookedAppPayment { get; set; }
        public DbSet<DoctorSpeciality> DoctorSpecialities { get; set; }
        public DbSet<ClinicCertificate> ClinicCertificate { get; set; }
        public DbSet<DoctorClinicSession> DoctorClinicSession { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ClinicOwner> ClinicOwner { get; set; }
        public DbSet<DoctorCalender>DoctorCalenders { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<PrescriptionMedicine> PrescriptionMedicine { get; set; }
        public DbSet<PrescriptionQuality> PrescriptionQuality { get; set; }
        public DbSet<Medicine> Medicine { get; set; }

    }
}

