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

// Identity yap�land�rmas�
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
// Veritaban� servislerini ekliyoruz (Ba�lant� ve veri eri�imi i�in gerekli)
builder.Services.AddScoped<IMalzemeDal, EfMalzemeRepo>();  // IMalzemeDal'� MalzemeDal'a ba�la
builder.Services.AddScoped<IMalzemeBirimDal, EfMalzemeBirimRepo>();  // IMalzemeBirimDal'� MalzemeBirimDal'a ba�la

builder.Services.AddScoped<IMalzemeGrupService, MalzemeGrupService>();
builder.Services.AddScoped<IMalzemeGrupDal, EfMalzemeGrupRepo>();


builder.Services.AddScoped<IMalzemeService, MalzemeService>();
builder.Services.AddScoped<IMalzemeGrupService, MalzemeGrupService>();
builder.Services.AddScoped<IMalzemeBirimService, MalzemeBirimService>();

builder.Services.AddScoped<IStokService,StokService>();
builder.Services.AddScoped<IStokDal, EfStokRepo>();



// Hata sayfalar� ve di�er ayarlar
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";  // Giri� sayfas�
    options.AccessDeniedPath = "/Account/AccessDenied";  // Eri�im reddedildi sayfas�
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
