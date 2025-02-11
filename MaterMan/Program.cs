using MaterMan.Business;
using MaterMan.Business.Abstract;
using MaterMan.Business.Concrete;
using MaterMan.Data;
using MaterMan.Data.Abstract;
using MaterMan.Data.EfRepository;
using MaterMan.Entity;
using MaterMan.Services.EmailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lant�s�
var connectionString = builder.Configuration.GetConnectionString("MaterManDbConnection");

// DbContext ve Identity yap�land�rmas� (AppUser ve AppRole kullan�yoruz)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Identity yap�land�rmas�: AppUser ve AppRole kullan�lacak
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();  // Identity yap�land�rmas�

builder.Services.AddSession(); // Session kullan�m� i�in ekle
builder.Services.AddSingleton<EmailService>(); // Email servisini ekle

// Ayr�ca, SignInManager ve UserManager servisleri eklenmeli
builder.Services.AddScoped<SignInManager<AppUser>>();
builder.Services.AddScoped<UserManager<AppUser>>();

// AutoMapper servisi
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Ba��ml�l�k enjeksiyonlar� (DI) 
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


//MBKur
builder.Services.AddHttpClient<CurrencyService>();

// Kimlik do�rulama yap�land�rmas�
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/Index";  // Giri� sayfas�
    options.AccessDeniedPath = "/Login/AccessDenied";  // Yetkisiz eri�im
});

// MVC yap�land�rmas�
builder.Services.AddControllersWithViews();

var app = builder.Build();

// HTTP request pipeline (Hata yakalama ve geli�tirme ortam� ayarlar�)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// HTTPS y�nlendirme ve static dosyalar
app.UseHttpsRedirection();
app.UseStaticFiles();

// Routing
app.UseRouting();

// Kimlik do�rulama ve yetkilendirme middleware'leri
app.UseSession(); // Session kullan�m� aktif et
app.UseRouting();
app.UseAuthorization();

// MVC controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
