using Infra;
using Infra.Repositories.Classes;
using Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var scon = builder.Configuration.GetConnectionString("scon");
builder.Services.AddDbContextPool<HospitalContext>(
    opt=>opt.UseLazyLoadingProxies().UseSqlServer(scon));

builder.Services.AddScoped<IAdmin,AdminRepo>();
builder.Services.AddScoped<ICountry, CountryRepo>();
builder.Services.AddScoped<IState, StateRepo>();
builder.Services.AddScoped<ICity, CityRepo>();
builder.Services.AddScoped<IArea, AreaRepo>();
builder.Services.AddScoped<ISpecility, SpecilityRepo>();
builder.Services.AddScoped<IUser, UserRepo>();
builder.Services.AddScoped<IClinic, ClinicRepo>();
builder.Services.AddScoped<IOPDSession,OPDSessionRepo>();
builder.Services.AddScoped<IClinicOwner, ClinicOwnerRepo>();
builder.Services.AddScoped<IClinicCertificate, ClinicCertificateRepo>();
builder.Services.AddScoped<IDoctor, DoctorRepo>();
builder.Services.AddScoped<IDoctorClinicSession, DoctorClinicSessionRepo>();
builder.Services.AddScoped<IDoctorCalender, DoctorCalenderRepo>();
builder.Services.AddScoped<IBookAppoinment ,BookAppoinmentRepo>();
builder.Services.AddScoped<IMedicine, MedicineRepo>();
builder.Services.AddScoped<IPatient, PatientRepo>();
builder.Services.AddScoped<IPrescribtion,PrescribtionRepo>();
builder.Services.AddScoped<IPrescribtionMedicine,PrescriptionMedicineRepo>();
builder.Services.AddScoped<IPrescribtionQuality,PrescriptionQualityRepo>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(20);
    opt.Cookie.IsEssential = true;
    opt.Cookie.HttpOnly = true;
});



var app = builder.Build();
app.UseStaticFiles();

app.UseSession();
   app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapDefaultControllerRoute();



app.Run();
