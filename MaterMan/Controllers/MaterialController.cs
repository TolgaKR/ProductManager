using AutoMapper;
using MaterMan.Business.Abstract;
using MaterMan.Business.Concrete;
using MaterMan.Entity.Concrete;
using MaterMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MaterMan.Controllers
{

    public class MaterialController : Controller
    {


        private readonly IMalzemeService _malzemeService;
        private readonly IMapper _mapper;
        private readonly IMalzemeGrupService _malzemeGrupService;
        private readonly IStokService _stokService;
        private readonly IMalzemeBirimService _malzemeBirimService;

        public MaterialController(IMalzemeService malzemeService, IMapper mapper, IMalzemeGrupService malzemeGrupService, IStokService stokService, IMalzemeBirimService malzemeBirimService)
        {
            _malzemeService = malzemeService;
            _mapper = mapper;
            _malzemeGrupService = malzemeGrupService;
            _stokService = stokService;
            _malzemeBirimService = malzemeBirimService;

        }

        #region AddMaterial
        [HttpGet]
        public async Task<IActionResult> AddMaterial()
        {
            // Malzeme Grupları ve Birimlerini Veritabanından Çek
            var malzemeGruplari = await _malzemeGrupService.GetAllMalzemeGrupAsync();
            var malzemeBirimleri = await _malzemeBirimService.GetAllMalzemeBirimAsync();

            // Eğer gruplar ve birimler boşsa hata mesajı ekleyelim
            if (malzemeGruplari == null || !malzemeGruplari.Any())
            {
                ModelState.AddModelError("", "Malzeme grupları yüklenemedi.");
            }

            if (malzemeBirimleri == null || !malzemeBirimleri.Any())
            {
                ModelState.AddModelError("", "Malzeme birimleri yüklenemedi.");
            }

            // ViewBag ile SelectList gönderiyoruz
            ViewBag.MalzemeGruplari = new SelectList(malzemeGruplari, "Id", "GrupAdi");
            ViewBag.MalzemeBirimleri = new SelectList(malzemeBirimleri, "Id", "BirimAdi");

            // View'e boş bir model gönderiyoruz (Kullanıcı giriş yapacak)
            return View(new MaterialViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddMaterial(MaterialViewModel model)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.MalzemeGruplari = new SelectList(await _malzemeGrupService.GetAllMalzemeGrupAsync(), "Id", "GrupAdi");
                ViewBag.MalzemeBirimleri = new SelectList(await _malzemeBirimService.GetAllMalzemeBirimAsync(), "Id", "BirimAdi");

                return View(model); // Hata varsa sayfayı tekrar göster
            }

            var newMaterial = new Malzeme
            {
                MalzemeAdi = model.MalzemeAdi,          // ViewModel'den gelen malzeme adı
                MalzemeGrupId = model.MalzemeGrupId,    // ViewModel'den gelen malzeme grup ID'si
                MalzemeBirimId = model.MalzemeBirimId,  // ViewModel'den gelen malzeme birimi ID'si
                StokMiktari = model.StokMiktari,       // ViewModel'den gelen stok miktarı
                                                       //FiyatMiktari = model.Fiyat,     
                IsActive = true                                        // Fiyat miktarını doğru şekilde atamalısınız
            };
            await _malzemeService.AddMalzemeAsync(newMaterial);



            var newStok = new Stok
            {
                MalzemeId = newMaterial.Id,
                IslemTipi = "Giris",
                IslemTarihi = DateTime.Now,
                StokAdet = Convert.ToInt32(model.StokMiktari)
            };
            try
            {

                await _stokService.AddStokAsync(newStok);
                ViewBag.Succeeded = "Malzeme başarıyla eklendi.";


                return RedirectToAction("Index"); // Basariyla eklendiyse, Index sayfasına yönlendir (Listeleme)
            }
            catch (Exception ex)
            {
                // Hata mesajını yazdırıyoruz
                Console.WriteLine("Exception Message: " + ex.Message);
                // InnerException varsa, daha fazla bilgi alıyoruz
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
                // Modeli yeniden döndürüyoruz
                return View(model);
            }

        }
        #endregion 


        #region Listeleme
        public async Task<IActionResult> Index()
        {

            var list = await _malzemeService.GetAllMalzemeAsync();


            return View(list);

        }
        #endregion 


        #region DeleteMaterial
        [HttpPost]
        public async Task<IActionResult> DeleteMaterial(int id)
        {

            await _malzemeService.DeleteMalzemeAsync(id);

            return RedirectToAction("Index");
        }
        #endregion



        [HttpGet]
        public async Task<IActionResult> UpdateMaterial(int id)
        { // Malzeme Grupları ve Birimlerini Veritabanından Çek
            var malzemeGruplari = await _malzemeGrupService.GetAllMalzemeGrupAsync();
            var malzemeBirimleri = await _malzemeBirimService.GetAllMalzemeBirimAsync();

            // Eğer gruplar ve birimler boşsa hata mesajı ekleyelim
            if (malzemeGruplari == null || !malzemeGruplari.Any())
            {
                ModelState.AddModelError("", "Malzeme grupları yüklenemedi.");
            }

            if (malzemeBirimleri == null || !malzemeBirimleri.Any())
            {
                ModelState.AddModelError("", "Malzeme birimleri yüklenemedi.");
            }

            // ViewBag ile SelectList gönderiyoruz
            ViewBag.MalzemeGruplari = new SelectList(malzemeGruplari, "Id", "GrupAdi");
            ViewBag.MalzemeBirimleri = new SelectList(malzemeBirimleri, "Id", "BirimAdi");


            var malzeme = await _malzemeService.GetByIdAs(id);


            // View'e boş bir model gönderiyoruz (Kullanıcı giriş yapacak)
            return View(malzeme);
        }



        #region UpdateMaterial


        [HttpPost]
        public async Task<IActionResult> UpdateMaterial(Malzeme malzeme)
        {
            malzeme.IsActive=true;
            await _malzemeService.UpdateMalzemeAsync(malzeme);

            return RedirectToAction("Index");

        }


        #endregion


    }
}
