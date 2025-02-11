using MaterMan.Business;
using MaterMan.Business.Abstract;
using MaterMan.Business.Concrete;
using MaterMan.Data;
using MaterMan.Data.Abstract;
using MaterMan.Data.EfRepository;
using MaterMan.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantýsý
var connectionString = builder.Configuration.GetConnectionString("MaterManDbConnection");

// DbContext ve Identity yapýlandýrmasý (AppUser ve AppRole kullanýyoruz)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Identity yapýlandýrmasý: AppUser ve AppRole kullanýlacak
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();  // Identity yapýlandýrmasý

// Ayrýca, SignInManager ve UserManager servisleri eklenmeli
builder.Services.AddScoped<SignInManager<AppUser>>();
builder.Services.AddScoped<UserManager<AppUser>>();

// AutoMapper servisi
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Baðýmlýlýk enjeksiyonlarý (DI) 
builder.Services.AddScoped<IMalzemeDal, EfMalzemeRepo>();
builder.Services.AddScoped<IMalzemeBirimDal, EfMalzemeBirimRepo>();
builder.Services.AddScoped<IMalzemeGrupDal, EfMalzemeGrupRepo>();
builder.Services.AddScoped<IReceteBaslikDal, EfReceteBaslikRepo>();
builder.Services.AddScoped<IReceteKalemDal, EfReceteKalemRepo>();
builder.Services.AddScoped<IStokDal, EfStokRepo>();

builder.Services.AddScoped<IMalzemeService, MalzemeService>();
builder.Services.AddScoped<IMalzemeGrupService, MalzemeGrupService>();
builder.Services.AddScoped<IMalzemeBirimService, MalzemeBirimService>();
builder.Services.AddScoped<IReceteBaslikService, ReceteBaslikService>();
builder.Services.AddScoped<IReceteKalemService, ReceteKalemService>();
builder.Services.AddScoped<IStokService, StokService>();

// Kimlik doðrulama yapýlandýrmasý
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/Index";  // Giriþ sayfasý
    options.AccessDeniedPath = "/Login/AccessDenied";  // Yetkisiz eriþim
});

// MVC yapýlandýrmasý
builder.Services.AddControllersWithViews();

var app = builder.Build();

// HTTP request pipeline (Hata yakalama ve geliþtirme ortamý ayarlarý)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// HTTPS yönlendirme ve static dosyalar
app.UseHttpsRedirection();
app.UseStaticFiles();

// Routing
app.UseRouting();

// Kimlik doðrulama ve yetkilendirme middleware'leri
app.UseAuthentication();  // Authentication middleware
app.UseAuthorization();   // Authorization middleware

// MVC controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
