using MaterMan.Business.Abstract;
using MaterMan.Data;
using MaterMan.Entity.Concrete;
using MaterMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace MaterMan.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;


    public class ReceteController : Controller
    {
        private readonly IMalzemeService _malzemeService;
        private readonly AppDbContext _context;

        public ReceteController(IMalzemeService malzemeService, AppDbContext context)
        {
            _malzemeService = malzemeService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CheckStock([FromBody] ReceteKalemViewModel model)
        {
            if (model == null || model.Miktar <= 0)
            {
                return BadRequest(new { error = "Geçersiz malzeme!" });
            }

            // Stok miktarını _malzemeService ile kontrol et
            var stokMiktari = await _malzemeService.GetStockByMalzemeIdAsync(model.MalzemeId);

            // stokMiktari null ise veya istenilen miktardan azsa hata döndür
            if (stokMiktari == null || stokMiktari < model.Miktar)
            {
                return BadRequest(new { error = "Stokta yeterli miktar yok!", mevcutStok = stokMiktari });
            }

            return Ok(new { message = "Yeterli stok var.", mevcutStok = stokMiktari });
        }


        ////Recete Olusturma Metodu
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] ReceteViewModel model)
        //{
        //    if (model == null || model.Kalemler == null || !model.Kalemler.Any())
        //    {
        //        return BadRequest(new { error = "Eksik veya hatalı veri gönderildi." });
        //    }

        //    bool receteVarMi = await _context.ReceteBasliklar.AnyAsync(r => r.ReceteIsmi == model.ReceteIsmi && r.VersiyonNo == model.VersiyonNo);

        //    if (receteVarMi)
        //    {
        //        return BadRequest(new { error = "Bu versiyon numarasıyla zaten bir reçete mevcut!" });
        //    }

        //    var stokYetersizMalzemeler = new List<object>();

        //    // 1️⃣ Stok kontrolü yap
        //    foreach (var kalem in model.Kalemler)
        //    {
        //        var stokMiktari = await _malzemeService.GetStockByMalzemeIdAsync(kalem.MalzemeId);
        //        if (stokMiktari < kalem.Miktar)
        //        {
        //            stokYetersizMalzemeler.Add(new { MalzemeId = kalem.MalzemeId, MevcutStok = stokMiktari });
        //        }
        //    }

        //    // 2️⃣ Eğer stok yetersizse işlemi iptal et
        //    if (stokYetersizMalzemeler.Any())
        //    {
        //        return BadRequest(new { error = "Bazı malzemelerin stok miktarı yetersiz!", detaylar = stokYetersizMalzemeler });
        //    }

        //    // 3️⃣ Stokları düş
        //    foreach (var kalem in model.Kalemler)
        //    {
        //        var malzeme = await _context.Malzemeler.FindAsync(kalem.MalzemeId);
        //        if (malzeme != null)
        //        {
        //            malzeme.StokMiktari -= kalem.Miktar;
        //            if (malzeme.StokMiktari < 0) // 🛑 Eğer stok negatif olursa hata ver   //Burada hata dönmeyecek eksik recete listesi.
        //            {
        //                return BadRequest(new { error = $"{malzeme.MalzemeAdi} için yetersiz stok! (Mevcut: {malzeme.StokMiktari + kalem.Miktar})" });
        //            }
        //            _context.Malzemeler.Update(malzeme);
        //        }
        //    }

        //    // 4️⃣ Reçete kaydet
        //    var recete = new ReceteBaslik
        //    {
        //        ReceteIsmi = model.ReceteIsmi,
        //        Aciklama = model.Aciklama,
        //        VersiyonNo = model.VersiyonNo

        //    };
        //    await _context.ReceteBasliklar.AddAsync(recete);
        //    await _context.SaveChangesAsync();

        //    var receteKalemler = model.Kalemler.Select(kalem => new ReceteKalem
        //    {
        //        ReceteId = recete.ReceteBaslikId,
        //        MalzemeId = kalem.MalzemeId,
        //        Miktar = kalem.Miktar
        //    }).ToList();

        //    await _context.ReceteKalemler.AddRangeAsync(receteKalemler);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { message = "Reçete başarıyla kaydedildi!", guncellenenStoklar = model.Kalemler });
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReceteViewModel model)
        {
            try
            {
                if (model == null || model.Kalemler == null || !model.Kalemler.Any())
                {
                    return BadRequest(new { error = "Eksik veya hatalı veri gönderildi." });
                }

                bool receteVarMi = await _context.ReceteBasliklar.AnyAsync(r => r.ReceteIsmi == model.ReceteIsmi && r.VersiyonNo == model.VersiyonNo);

                if (receteVarMi)
                {
                    return BadRequest(new { error = "Bu versiyon numarasıyla zaten bir reçete mevcut!" });
                }

                var stokYetersizMalzemeler = new List<object>();

                // 1️⃣ Stok kontrolü yap
                foreach (var kalem in model.Kalemler)
                {
                    var stokMiktari = await _malzemeService.GetStockByMalzemeIdAsync(kalem.MalzemeId);
                    if (stokMiktari < kalem.Miktar)
                    {
                        stokYetersizMalzemeler.Add(new { MalzemeId = kalem.MalzemeId, MevcutStok = stokMiktari });
                    }
                }

                // 2️⃣ Eğer stok yetersizse işlemi iptal et
                if (stokYetersizMalzemeler.Any())
                {
                    return BadRequest(new { error = "Bazı malzemelerin stok miktarı yetersiz!", detaylar = stokYetersizMalzemeler });
                }

                // 3️⃣ Stokları düş
                foreach (var kalem in model.Kalemler)
                {
                    var malzeme = await _context.Malzemeler.FindAsync(kalem.MalzemeId);
                    if (malzeme != null)
                    {
                        malzeme.StokMiktari -= kalem.Miktar;
                        if (malzeme.StokMiktari < 0)
                        {
                            return BadRequest(new { error = $"{malzeme.MalzemeAdi} için yetersiz stok! (Mevcut: {malzeme.StokMiktari + kalem.Miktar})" });
                        }
                        _context.Malzemeler.Update(malzeme);
                    }
                }

                // 4️⃣ Reçete kaydet
                var recete = new ReceteBaslik
                {
                    ReceteIsmi = model.ReceteIsmi,
                    Aciklama = model.Aciklama,
                    VersiyonNo = model.VersiyonNo,
                    IsActive = true
                };
                await _context.ReceteBasliklar.AddAsync(recete);
                await _context.SaveChangesAsync();

                var receteKalemler = model.Kalemler.Select(kalem => new ReceteKalem
                {
                    ReceteBaslikId = recete.ReceteBaslikId, // ✅ Doğru alanı kullan
                    MalzemeId = kalem.MalzemeId,
                    Miktar = kalem.Miktar
                    
                }).ToList();

                

                await _context.ReceteKalemler.AddRangeAsync(receteKalemler);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Reçete başarıyla kaydedildi!", guncellenenStoklar = model.Kalemler });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Beklenmeyen bir hata oluştu!", detay = ex.Message });
            }
        }



        public async Task<IActionResult> AddRecete()
        {
            var result = await _malzemeService.GetAllMalzemeAsync();
            return View(result);
        }

        //ReceteListelemek için
        public async Task<IActionResult> ReceteList()
        {
            
            var result = await _context.ReceteBasliklar.ToListAsync();
            return View(result);
        }


        //Recete Silmek İçin

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var recete = await _context.ReceteBasliklar.FindAsync(id);
            if (recete == null)
            {
                return Json(new { success = false, error = "Reçete bulunamadı!" });
            }

            _context.ReceteBasliklar.Remove(recete);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }



        //Recete Detayı için

        public async Task<IActionResult> Detail(int id)
        {
            var recete = await _context.ReceteBasliklar.FindAsync(id);
            if (recete == null)
            {
                return NotFound();
            }
             var receteKalemler= await _context.ReceteKalemler.Where(rk => rk.ReceteKalemId == id).ToListAsync();
            var model = new ReceteViewModel
            {
                ReceteIsmi = recete.ReceteIsmi,
                Aciklama = recete.Aciklama,
                VersiyonNo = recete.VersiyonNo,
                Kalemler = receteKalemler.Select(rk => new ReceteKalemViewModel
                {
                    ReceteKalemId = rk.ReceteKalemId,
                    MalzemeId = rk.MalzemeId,
                    
                    Miktar = rk.Miktar
                }).ToList()
            };
            return View(model);
        }







    }
}
