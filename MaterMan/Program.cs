using MaterMan.Business;
using MaterMan.Business.Abstract;
using MaterMan.Business.Concrete;
using MaterMan.ChatHub;
using MaterMan.Data;
using MaterMan.Data.Abstract;
using MaterMan.Data.EfRepository;
using MaterMan.Entity;
using MaterMan.Services.EmailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ?? Veritabaný baðlantýsý
var connectionString = builder.Configuration.GetConnectionString("MaterManDbConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// ?? Identity yapýlandýrmasý
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// ?? Servisler ve baðýmlýlýk enjeksiyonu (DI)
builder.Services.AddSession();
builder.Services.AddSingleton<EmailService>();
builder.Services.AddScoped<SignInManager<AppUser>>();
builder.Services.AddScoped<UserManager<AppUser>>();

// ?? AutoMapper servisi
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingProfile));

// ?? Repository ve Servis Baðýmlýlýklarý
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

// ?? SignalR servisini ekle
builder.Services.AddSignalR();

// ?? Döviz kuru servisi
builder.Services.AddHttpClient<CurrencyService>();

// ?? Kimlik doðrulama yapýlandýrmasý
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/Index";
    options.AccessDeniedPath = "/Login/AccessDenied";
});

// ?? MVC yapýlandýrmasý
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ?? Geliþtirme ve hata yakalama ayarlarý
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// ?? HTTPS yönlendirme, statik dosyalar ve Session kullanýmý
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

// ?? Middleware sýralamasý düzeltilmiþ
app.UseRouting();  // ? ÖNCE Routing çaðrýlmalý!
app.UseAuthorization();

// ?? MVC ve SignalR Route tanýmlamalarý
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chatHub"); // ? SignalR Hub'ý
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Login}/{action=Index}/{id?}");
});

app.Run();
