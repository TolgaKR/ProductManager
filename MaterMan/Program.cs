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

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("MaterManDbConnection");


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingProfile));


// Identity ve DbContext servislerini ekle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Identity yapýlandýrmasý
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
// Veritabaný servislerini ekliyoruz (Baðlantý ve veri eriþimi için gerekli)
builder.Services.AddScoped<IMalzemeDal, EfMalzemeRepo>();  // IMalzemeDal'ý MalzemeDal'a baðla
builder.Services.AddScoped<IMalzemeBirimDal, EfMalzemeBirimRepo>();  // IMalzemeBirimDal'ý MalzemeBirimDal'a baðla

builder.Services.AddScoped<IMalzemeGrupService, MalzemeGrupService>();
builder.Services.AddScoped<IMalzemeGrupDal, EfMalzemeGrupRepo>();


builder.Services.AddScoped<IMalzemeService, MalzemeService>();
builder.Services.AddScoped<IMalzemeGrupService, MalzemeGrupService>();
builder.Services.AddScoped<IMalzemeBirimService, MalzemeBirimService>();

builder.Services.AddScoped<IStokService,StokService>();
builder.Services.AddScoped<IStokDal, EfStokRepo>();



// Hata sayfalarý ve diðer ayarlar
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";  // Giriþ sayfasý
    options.AccessDeniedPath = "/Account/AccessDenied";  // Eriþim reddedildi sayfasý
});

var app = builder.Build();

// HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // Authentication middleware
app.UseAuthorization();   // Authorization middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Recete}/{action=AddRecete}/{id?}");

app.Run();
