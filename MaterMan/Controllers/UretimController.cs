using AspNetCoreGeneratedDocument;
using MaterMan.Business.Abstract;
using MaterMan.Data;
using MaterMan.Entity.Concrete;
using MaterMan.Entity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaterMan.Controllers
{
    public class UretimController : Controller
    {
        private readonly IMalzemeService _malzemeservice;
        private readonly IMalzemeBirimService _malzemebirimService;
        private readonly IMalzemeGrupService _malzemegrupService;
        private readonly AppDbContext _context; // DbContext ekle

        public UretimController(
            IMalzemeService malzemeService,
            IMalzemeBirimService malzemeBirimService,
            IMalzemeGrupService malzemeGrupService,
            AppDbContext context) // DbContext burada enjekte ediliyor
        {
            _malzemeservice = malzemeService;
            _malzemebirimService = malzemeBirimService;
            _malzemegrupService = malzemeGrupService;
            _context = context; // DbContext'i burada set et
        }

        [HttpPost]
        public async Task<IActionResult> Uretim(int id, decimal stokmiktari)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1️⃣ Önce, gelen `id` ReceteBaslikId olduğundan ilgili MalzemeId'yi al
                var malzemeId = await _context.ReceteBasliklar
                    .Where(rb => rb.ReceteBaslikId == id)
                    .Select(rb => rb.MalzemeId)
                    .FirstOrDefaultAsync();

                if (malzemeId == null)
                    return Json(new { success = false, message = "Bağlı malzeme bulunamadı!" });

                // 2️⃣ Seçilen MalzemeId'ye göre reçete kalemlerini al
                var receteKalemleri = await _context.ReceteKalemler
                    .Where(r => r.ReceteBaslikId == id)
                    .Include(r => r.Malzeme)
                    .ToListAsync();

                if (!receteKalemleri.Any())
                    return Json(new { success = false, message = "Reçete bulunamadı!" });

                // 3️⃣ Gerekli malzemelerin miktarlarını hesapla
                var gerekliMalzemeler = receteKalemleri
                    .GroupBy(r => r.MalzemeId)
                    .Select(g => new
                    {
                        MalzemeId = g.Key,
                        ToplamGerekliMiktar = g.Sum(r => r.Miktar * stokmiktari)
                    })
                    .ToList();

                // 4️⃣ Stokları kontrol et
                var stoklar = await _context.Stoklar
                    .Where(s => gerekliMalzemeler.Select(gm => gm.MalzemeId).Contains(s.MalzemeId))
                    .ToDictionaryAsync(s => s.MalzemeId, s => s.StokAdet);

                var eksikMalzemeler = new List<string>();

                foreach (var item in gerekliMalzemeler)
                {
                    if (!stoklar.ContainsKey(item.MalzemeId) || stoklar[item.MalzemeId] < item.ToplamGerekliMiktar)
                    {
                        var malzemeAdi = await _context.Malzemeler
                            .Where(m => m.Id == item.MalzemeId)
                            .Select(m => m.MalzemeAdi)
                            .FirstOrDefaultAsync();

                        eksikMalzemeler.Add(malzemeAdi ?? $"MalzemeID {item.MalzemeId}");
                    }
                }

                if (eksikMalzemeler.Any())
                    return Json(new { success = false, missingItems = eksikMalzemeler });

                // 5️⃣ Stokları Güncelle (Hammaddeyi düş, ürünü ekle)
                foreach (var item in gerekliMalzemeler)
                {
                    var stok = await _context.Stoklar.FirstOrDefaultAsync(s => s.MalzemeId == item.MalzemeId);
                    if (stok != null)
                    {
                        stok.StokAdet -= item.ToplamGerekliMiktar; // Hammaddeyi stoktan düş
                    }

                    // 🔴 **Malzeme tablosundaki StokMiktari'ni de güncelle!**
                    var hammaddeMalzeme = await _context.Malzemeler.FirstOrDefaultAsync(m => m.Id == item.MalzemeId);
                    if (hammaddeMalzeme != null && hammaddeMalzeme.StokMiktari >= item.ToplamGerekliMiktar)
                    {
                        hammaddeMalzeme.StokMiktari -= item.ToplamGerekliMiktar; // 🔴 Hammaddeden düş
                    }
                }

                // 6️⃣ Üretilen Malzemeyi **Stok** tablosuna ekle
                var uretilenMalzemeStok = await _context.Stoklar.FirstOrDefaultAsync(s => s.MalzemeId == malzemeId);
                if (uretilenMalzemeStok != null)
                {
                    uretilenMalzemeStok.StokAdet += stokmiktari; // Üretilen ürünü ekle
                }
                else
                {
                    _context.Stoklar.Add(new Stok
                    {
                        MalzemeId = malzemeId,
                        StokAdet = stokmiktari
                    });
                }

                // 7️⃣ **Malzeme** tablosundaki StokMiktari alanını güncelle
                var uretilenMalzeme = await _context.Malzemeler.FirstOrDefaultAsync(m => m.Id == malzemeId);
                if (uretilenMalzeme != null)
                {
                    uretilenMalzeme.StokMiktari += stokmiktari;
                }

                // 8️⃣ Veritabanını Güncelle
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Json(new { success = true, message = "Üretim başarıyla tamamlandı!" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Json(new { success = false, message = $"Hata: {ex.Message}" });
            }
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var receteler = await _context.ReceteBasliklar.ToListAsync();

            var model = new ReceteDetayViewModel
            {
                receteBaslik = receteler
            };

            return View(model);
        }

    }

}
