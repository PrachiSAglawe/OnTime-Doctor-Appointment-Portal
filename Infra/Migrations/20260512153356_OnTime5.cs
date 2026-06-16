using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class OnTime5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    MedicineID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenericName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicineTypes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.MedicineID);
                });

            migrationBuilder.CreateTable(
                name: "Specilities",
                columns: table => new
                {
                    SpecilityID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecilityName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specilities", x => x.SpecilityID);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateID);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Citys",
                columns: table => new
                {
                    CityID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citys", x => x.CityID);
                    table.ForeignKey(
                        name: "FK_Citys_States_StateID",
                        column: x => x.StateID,
                        principalTable: "States",
                        principalColumn: "StateID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    EmailID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientID);
                    table.ForeignKey(
                        name: "FK_Patients_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    AreaID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.AreaID);
                    table.ForeignKey(
                        name: "FK_Areas_Citys_CityID",
                        column: x => x.CityID,
                        principalTable: "Citys",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    ClinicID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandLineNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.ClinicID);
                    table.ForeignKey(
                        name: "FK_Clinics_Citys_CityID",
                        column: x => x.CityID,
                        principalTable: "Citys",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClinicCertificate",
                columns: table => new
                {
                    ClinicCertificateID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicID = table.Column<long>(type: "bigint", nullable: false),
                    CertificateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertificateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicCertificate", x => x.ClinicCertificateID);
                    table.ForeignKey(
                        name: "FK_ClinicCertificate_Clinics_ClinicID",
                        column: x => x.ClinicID,
                        principalTable: "Clinics",
                        principalColumn: "ClinicID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClinicFacilities",
                columns: table => new
                {
                    ClinicFacilityID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicFacilities", x => x.ClinicFacilityID);
                    table.ForeignKey(
                        name: "FK_ClinicFacilities_Clinics_ClinicID",
                        column: x => x.ClinicID,
                        principalTable: "Clinics",
                        principalColumn: "ClinicID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClinicOwner",
                columns: table => new
                {
                    ClinicOwnerID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicOwner", x => x.ClinicOwnerID);
                    table.ForeignKey(
                        name: "FK_ClinicOwner_Clinics_ClinicID",
                        column: x => x.ClinicID,
                        principalTable: "Clinics",
                        principalColumn: "ClinicID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClinicRatings",
                columns: table => new
                {
                    ClinicRatingID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicID = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicRatings", x => x.ClinicRatingID);
                    table.ForeignKey(
                        name: "FK_ClinicRatings_Clinics_ClinicID",
                        column: x => x.ClinicID,
                        principalTable: "Clinics",
                        principalColumn: "ClinicID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "doctors",
                columns: table => new
                {
                    DoctorID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OPDFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DoctorExperience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaID = table.Column<long>(type: "bigint", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorQualification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctors", x => x.DoctorID);
                    table.ForeignKey(
                        name: "FK_doctors_Areas_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Areas",
                        principalColumn: "AreaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_doctors_Clinics_ClinicID",
                        column: x => x.ClinicID,
                        principalTable: "Clinics",
                        principalColumn: "ClinicID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OPDSessions",
                columns: table => new
                {
                    OPDSessionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OPDSessions", x => x.OPDSessionID);
                    table.ForeignKey(
                        name: "FK_OPDSessions_Clinics_ClinicID",
                        column: x => x.ClinicID,
                        principalTable: "Clinics",
                        principalColumn: "ClinicID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorRatings",
                columns: table => new
                {
                    DoctorRatingID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorID = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorRatings", x => x.DoctorRatingID);
                    table.ForeignKey(
                        name: "FK_DoctorRatings_doctors_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "doctors",
                        principalColumn: "DoctorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSpecialities",
                columns: table => new
                {
                    DoctorSpecialityID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorID = table.Column<long>(type: "bigint", nullable: false),
                    SpecilityID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSpecialities", x => x.DoctorSpecialityID);
                    table.ForeignKey(
                        name: "FK_DoctorSpecialities_Specilities_SpecilityID",
                        column: x => x.SpecilityID,
                        principalTable: "Specilities",
                        principalColumn: "SpecilityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorSpecialities_doctors_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "doctors",
                        principalColumn: "DoctorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorClinicSession",
                columns: table => new
                {
                    DoctorClinicSessionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorID = table.Column<long>(type: "bigint", nullable: false),
                    ClinicID = table.Column<long>(type: "bigint", nullable: false),
                    OPDSessionID = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeInterval = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorClinicSession", x => x.DoctorClinicSessionID);
                    table.ForeignKey(
                        name: "FK_DoctorClinicSession_Clinics_ClinicID",
                        column: x => x.ClinicID,
                        principalTable: "Clinics",
                        principalColumn: "ClinicID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorClinicSession_OPDSessions_OPDSessionID",
                        column: x => x.OPDSessionID,
                        principalTable: "OPDSessions",
                        principalColumn: "OPDSessionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorClinicSession_doctors_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "doctors",
                        principalColumn: "DoctorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookedAppointments",
                columns: table => new
                {
                    BookedAppointmentsID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorClinicSessionID = table.Column<long>(type: "bigint", nullable: false),
                    PatientID = table.Column<long>(type: "bigint", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookedAppointments", x => x.BookedAppointmentsID);
                    table.ForeignKey(
                        name: "FK_BookedAppointments_DoctorClinicSession_DoctorClinicSessionID",
                        column: x => x.DoctorClinicSessionID,
                        principalTable: "DoctorClinicSession",
                        principalColumn: "DoctorClinicSessionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookedAppointments_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorCalenders",
                columns: table => new
                {
                    DoctorCalenderID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorClinicSessionID = table.Column<long>(type: "bigint", nullable: false),
                    Fromdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Todate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorCalenders", x => x.DoctorCalenderID);
                    table.ForeignKey(
                        name: "FK_DoctorCalenders_DoctorClinicSession_DoctorClinicSessionID",
                        column: x => x.DoctorClinicSessionID,
                        principalTable: "DoctorClinicSession",
                        principalColumn: "DoctorClinicSessionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookedAppPayment",
                columns: table => new
                {
                    BookedAppPaymentID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookedAppointmentsID = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookedAppPayment", x => x.BookedAppPaymentID);
                    table.ForeignKey(
                        name: "FK_BookedAppPayment_BookedAppointments_BookedAppointmentsID",
                        column: x => x.BookedAppointmentsID,
                        principalTable: "BookedAppointments",
                        principalColumn: "BookedAppointmentsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    PrescriptionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookedAppointmentsID = table.Column<long>(type: "bigint", nullable: false),
                    DoctorID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.PrescriptionID);
                    table.ForeignKey(
                        name: "FK_Prescription_BookedAppointments_BookedAppointmentsID",
                        column: x => x.BookedAppointmentsID,
                        principalTable: "BookedAppointments",
                        principalColumn: "BookedAppointmentsID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prescription_doctors_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "doctors",
                        principalColumn: "DoctorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionMedicine",
                columns: table => new
                {
                    PrescriptionMedicineID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    MedicineID = table.Column<long>(type: "bigint", nullable: false),
                    PrescriptionID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionMedicine", x => x.PrescriptionMedicineID);
                    table.ForeignKey(
                        name: "FK_PrescriptionMedicine_Medicine_MedicineID",
                        column: x => x.MedicineID,
                        principalTable: "Medicine",
                        principalColumn: "MedicineID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrescriptionMedicine_Prescription_PrescriptionID",
                        column: x => x.PrescriptionID,
                        principalTable: "Prescription",
                        principalColumn: "PrescriptionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionQuality",
                columns: table => new
                {
                    PrescriptionQualityID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionID = table.Column<long>(type: "bigint", nullable: false),
                    NextVisit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Suggestion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionQuality", x => x.PrescriptionQualityID);
                    table.ForeignKey(
                        name: "FK_PrescriptionQuality_Prescription_PrescriptionID",
                        column: x => x.PrescriptionID,
                        principalTable: "Prescription",
                        principalColumn: "PrescriptionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminID", "EmailID", "FirstName", "LastName", "MobileNo", "Password" },
                values: new object[,]
                {
                    { 1L, "sunil@hotmail.com", "Sunil", "Pawar", "9373750490", "abcd" },
                    { 2L, "Anil12@gmail.com", "Anil", "Sumbe", "9373750490", "abcd" },
                    { 3L, "mahesh@gmail.com", "Mahesh", "Tambe", "9373751234", "1234" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "CountryName" },
                values: new object[] { 1L, "India" });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateID", "CountryID", "StateName" },
                values: new object[] { 1L, 1L, "Maharashtra" });

            migrationBuilder.InsertData(
                table: "Citys",
                columns: new[] { "CityID", "CityName", "StateID" },
                values: new object[] { 1L, "Pune", 1L });

            migrationBuilder.InsertData(
                table: "Clinics",
                columns: new[] { "ClinicID", "Address", "CityID", "ClinicName", "ContactPersonName", "EmailID", "LandLineNo", "MobileNo", "WebsiteUrl" },
                values: new object[] { 1L, "Akurdi Pune", 1L, "LifeCare", "Admin", "lifecare@gmail.com", "020123456", "9876543210", "http://lifecare.com" });

            migrationBuilder.InsertData(
                table: "ClinicOwner",
                columns: new[] { "ClinicOwnerID", "ClinicID", "EmailID", "FirstName", "Gender", "LastName", "MobileNo", "Password" },
                values: new object[] { 1L, 1L, "Chaitali@gmail.com", "Chaitali", "Female", "Mahandule", "9078563490", "abcd" });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_CityID",
                table: "Areas",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_BookedAppointments_DoctorClinicSessionID",
                table: "BookedAppointments",
                column: "DoctorClinicSessionID");

            migrationBuilder.CreateIndex(
                name: "IX_BookedAppointments_PatientID",
                table: "BookedAppointments",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_BookedAppPayment_BookedAppointmentsID",
                table: "BookedAppPayment",
                column: "BookedAppointmentsID");

            migrationBuilder.CreateIndex(
                name: "IX_Citys_StateID",
                table: "Citys",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicCertificate_ClinicID",
                table: "ClinicCertificate",
                column: "ClinicID");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicFacilities_ClinicID",
                table: "ClinicFacilities",
                column: "ClinicID");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicOwner_ClinicID",
                table: "ClinicOwner",
                column: "ClinicID");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicRatings_ClinicID",
                table: "ClinicRatings",
                column: "ClinicID");

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_CityID",
                table: "Clinics",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorCalenders_DoctorClinicSessionID",
                table: "DoctorCalenders",
                column: "DoctorClinicSessionID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorClinicSession_ClinicID",
                table: "DoctorClinicSession",
                column: "ClinicID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorClinicSession_DoctorID",
                table: "DoctorClinicSession",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorClinicSession_OPDSessionID",
                table: "DoctorClinicSession",
                column: "OPDSessionID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorRatings_DoctorID",
                table: "DoctorRatings",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_doctors_AreaID",
                table: "doctors",
                column: "AreaID");

            migrationBuilder.CreateIndex(
                name: "IX_doctors_ClinicID",
                table: "doctors",
                column: "ClinicID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpecialities_DoctorID",
                table: "DoctorSpecialities",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpecialities_SpecilityID",
                table: "DoctorSpecialities",
                column: "SpecilityID");

            migrationBuilder.CreateIndex(
                name: "IX_OPDSessions_ClinicID",
                table: "OPDSessions",
                column: "ClinicID");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserID",
                table: "Patients",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_BookedAppointmentsID",
                table: "Prescription",
                column: "BookedAppointmentsID");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_DoctorID",
                table: "Prescription",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedicine_MedicineID",
                table: "PrescriptionMedicine",
                column: "MedicineID");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedicine_PrescriptionID",
                table: "PrescriptionMedicine",
                column: "PrescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionQuality_PrescriptionID",
                table: "PrescriptionQuality",
                column: "PrescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryID",
                table: "States",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryID",
                table: "Users",
                column: "CountryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "BookedAppPayment");

            migrationBuilder.DropTable(
                name: "ClinicCertificate");

            migrationBuilder.DropTable(
                name: "ClinicFacilities");

            migrationBuilder.DropTable(
                name: "ClinicOwner");

            migrationBuilder.DropTable(
                name: "ClinicRatings");

            migrationBuilder.DropTable(
                name: "DoctorCalenders");

            migrationBuilder.DropTable(
                name: "DoctorRatings");

            migrationBuilder.DropTable(
                name: "DoctorSpecialities");

            migrationBuilder.DropTable(
                name: "PrescriptionMedicine");

            migrationBuilder.DropTable(
                name: "PrescriptionQuality");

            migrationBuilder.DropTable(
                name: "Specilities");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "BookedAppointments");

            migrationBuilder.DropTable(
                name: "DoctorClinicSession");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "OPDSessions");

            migrationBuilder.DropTable(
                name: "doctors");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "Citys");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
