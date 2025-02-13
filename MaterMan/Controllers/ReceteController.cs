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
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.DotNet.Scaffolding.Shared.Messaging;
    using OfficeOpenXml;
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
            var stokMiktari = await _malzemeService.GetStokByMalzemeIdAsync(model.MalzemeId);

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

        //#region Create2
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] ReceteViewModel model)
        //{
        //    try
        //    {
        //        if (model == null || model.Kalemler == null || !model.Kalemler.Any())
        //        {
        //            return BadRequest(new { error = "Eksik veya hatalı veri gönderildi." });
        //        }

        //        bool receteVarMi = await _context.ReceteBasliklar.AnyAsync(r => r.ReceteIsmi == model.ReceteIsmi && r.VersiyonNo == model.VersiyonNo);

        //        if (receteVarMi)
        //        {
        //            return BadRequest(new { error = "Bu versiyon numarasıyla zaten bir reçete mevcut!" });
        //        }

        //        var stokYetersizMalzemeler = new List<object>();

        //        // 1️⃣ Stok kontrolü yap
        //        foreach (var kalem in model.Kalemler)
        //        {
        //            var stokMiktari = await _malzemeService.GetStockByMalzemeIdAsync(kalem.MalzemeId);
        //            if (stokMiktari < kalem.Miktar)
        //            {
        //                stokYetersizMalzemeler.Add(new { MalzemeId = kalem.MalzemeId, MevcutStok = stokMiktari });
        //            }
        //        }

        //        // 2️⃣ Eğer stok yetersizse işlemi iptal et
        //        if (stokYetersizMalzemeler.Any())
        //        {
        //            return BadRequest(new { error = "Bazı malzemelerin stok miktarı yetersiz!", detaylar = stokYetersizMalzemeler });
        //        }

        //        // 3️⃣ Stokları düş
        //        foreach (var kalem in model.Kalemler)
        //        {
        //            var malzeme = await _context.Malzemeler.FindAsync(kalem.MalzemeId);
        //            if (malzeme != null)
        //            {
        //                malzeme.StokMiktari -= kalem.Miktar;
        //                if (malzeme.StokMiktari < 0)
        //                {
        //                    return BadRequest(new { error = $"{malzeme.MalzemeAdi} için yetersiz stok! (Mevcut: {malzeme.StokMiktari + kalem.Miktar})" });
        //                }
        //                _context.Malzemeler.Update(malzeme);
        //            }
        //        }

        //        // 4️⃣ Reçete kaydet
        //        var recete = new ReceteBaslik
        //        {
        //            ReceteIsmi = model.ReceteIsmi,
        //            Aciklama = model.Aciklama,
        //            VersiyonNo = model.VersiyonNo,
        //            IsActive = true
        //        };
        //        await _context.ReceteBasliklar.AddAsync(recete);
        //        await _context.SaveChangesAsync();

        //        var receteKalemler = model.Kalemler.Select(kalem => new ReceteKalem
        //        {
        //            ReceteBaslikId = recete.ReceteBaslikId, // ✅ Doğru alanı kullan
        //            MalzemeId = kalem.MalzemeId,
        //            Miktar = kalem.Miktar

        //        }).ToList();



        //        await _context.ReceteKalemler.AddRangeAsync(receteKalemler);
        //        await _context.SaveChangesAsync();

        //        return Ok(new { message = "Reçete başarıyla kaydedildi!", guncellenenStoklar = model.Kalemler });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { error = "Beklenmeyen bir hata oluştu!", detay = ex.Message });
        //    }
        //}
        //#endregion

        #region Create22
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] ReceteViewModel model)
        //{
        //    if (model == null || model.Kalemler == null || !model.Kalemler.Any())
        //    {
        //        return BadRequest(new { error = "Eksik veya hatalı veri gönderildi." });
        //    }

        //    using (var transaction = await _context.Database.BeginTransactionAsync()) // 🔥 Transaction başlat
        //    {
        //        try
        //        {
        //            bool receteVarMi = await _context.ReceteBasliklar.AnyAsync(r =>
        //                r.ReceteIsmi == model.ReceteIsmi && r.VersiyonNo == model.VersiyonNo);

        //            if (receteVarMi)
        //            {
        //                return BadRequest(new { error = "Bu versiyon numarasıyla zaten bir reçete mevcut!" });
        //            }

        //            var stokYetersizMalzemeler = new List<object>();

        //            // 1️⃣ Stok kontrolü yap
        //            foreach (var kalem in model.Kalemler)
        //            {
        //                var stokMiktari = await _malzemeService.GetStockByMalzemeIdAsync(kalem.MalzemeId);
        //                if (stokMiktari < kalem.Miktar)
        //                {
        //                    stokYetersizMalzemeler.Add(new { MalzemeId = kalem.MalzemeId, MevcutStok = stokMiktari });
        //                }
        //            }

        //            // 2️⃣ Eğer stok yetersizse işlemi iptal et
        //            if (stokYetersizMalzemeler.Any())
        //            {
        //                return BadRequest(new { error = "Bazı malzemelerin stok miktarı yetersiz!", detaylar = stokYetersizMalzemeler });
        //            }

        //            // 3️⃣ Stokları düş
        //            foreach (var kalem in model.Kalemler)
        //            {
        //                var malzeme = await _context.Malzemeler.FindAsync(kalem.MalzemeId);
        //                if (malzeme != null)
        //                {
        //                    malzeme.StokMiktari -= kalem.Miktar;
        //                    if (malzeme.StokMiktari < 0)
        //                    {
        //                        return BadRequest(new { error = $"{malzeme.MalzemeAdi} için yetersiz stok! (Mevcut: {malzeme.StokMiktari + kalem.Miktar})" });
        //                    }
        //                    _context.Malzemeler.Update(malzeme);
        //                }
        //            }

        //            // 4️⃣ Reçete kaydet
        //                var recete = new ReceteBaslik
        //                {
        //                    ReceteIsmi = model.ReceteIsmi,
        //                    Aciklama = model.Aciklama,
        //                    VersiyonNo = model.VersiyonNo,
        //                    MalzemeId=model.MalzemeId,
        //                    IsActive = true
        //                };

        //            await _context.ReceteBasliklar.AddAsync(recete);
        //            await _context.SaveChangesAsync();

        //            var receteKalemler = model.Kalemler.Select(kalem => new ReceteKalem
        //            {

        //                MalzemeId = kalem.MalzemeId,  // Burada doğru MalzemeId'yi kullanıyoruz
        //                Miktar = kalem.Miktar,
        //                ReceteBaslikId = recete.ReceteBaslikId // ReceteBaşlıkId'yi de eklemelisiniz
        //            }).ToList();


        //            await _context.ReceteKalemler.AddRangeAsync(receteKalemler);
        //            await _context.SaveChangesAsync();

        //            await transaction.CommitAsync(); //  Tüm işlemler başarılıysa commit et

        //            return Ok(new { message = "Reçete başarıyla kaydedildi!", guncellenenStoklar = model.Kalemler });
        //        }
        //        catch (DbUpdateException ex)
        //        {
        //            var errorDetails = ex.InnerException?.Message ?? ex.Message;
        //            return StatusCode(500, new { error = "Beklenmeyen bir hata oluştu!", detay = errorDetails });
        //        }
        //    }
        //}
        #endregion


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReceteViewModel model)
        {
            if (model == null || model.Kalemler == null || !model.Kalemler.Any())
            {
                return BadRequest(new { error = "Eksik veya hatalı veri gönderildi." });
            }
            using (var transaction = await _context.Database.BeginTransactionAsync()) // 🔥 Transaction başlat
            {
                try
                {
                    // 1️⃣ Mevcut en yüksek versiyon numarasını al
                    var mevcutRecete = await _context.ReceteBasliklar
                        .Where(r => r.MalzemeId == model.MalzemeId)
                        .OrderByDescending(r => r.VersiyonNo)
                        .FirstOrDefaultAsync();

                    int yeniVersiyonNo = mevcutRecete != null ? mevcutRecete.VersiyonNo + 1 : 1; // Eğer yoksa ilk versiyon

                    // 2️⃣ Eğer eski bir reçete varsa, onu pasif hale getir
                    if (mevcutRecete != null)
                    {
                        mevcutRecete.IsActive = false; // Eski reçeteyi pasif yap
                        _context.ReceteBasliklar.Update(mevcutRecete);
                    }

                    // 3️⃣ Yeni reçeteyi oluştur
                    var yeniRecete = new ReceteBaslik
                    {
                        ReceteIsmi = model.ReceteIsmi,
                        Aciklama = model.Aciklama,
                        VersiyonNo = yeniVersiyonNo,
                        MalzemeId = model.MalzemeId,
                        EklemeTarihi = DateTime.Now,
                        IsActive = true // Yeni reçete aktif olacak
                    };

                    await _context.ReceteBasliklar.AddAsync(yeniRecete);
                    await _context.SaveChangesAsync();

                    // 4️⃣ Yeni reçete kalemlerini ekle
                    if (model.Kalemler != null && model.Kalemler.Any())
                    {
                        var receteKalemler = model.Kalemler.Select(kalem => new ReceteKalem
                        {
                            MalzemeId = kalem.MalzemeId,
                            Miktar = kalem.Miktar,
                            ReceteBaslikId = yeniRecete.ReceteBaslikId
                        }).ToList();

                        await _context.ReceteKalemler.AddRangeAsync(receteKalemler);
                        await _context.SaveChangesAsync();
                    }

                    await transaction.CommitAsync(); // 🟢 Tüm işlemler başarılıysa commit et

                    return Ok(new
                    {
                        message = "Yeni reçete başarıyla oluşturuldu!",
                        yeniVersiyonNo,
                        eskiRecete = mevcutRecete != null ? $"Versiyon {mevcutRecete.VersiyonNo} pasif hale getirildi." : "İlk versiyon oluşturuldu."
                    });
                }
                catch (DbUpdateException ex)
                {
                    await transaction.RollbackAsync(); // 🔴 Hata olursa işlemi geri al
                    var errorDetails = ex.InnerException?.Message ?? ex.Message;
                    return StatusCode(500, new { error = "Beklenmeyen bir hata oluştu!", detay = errorDetails });
                }
            }
        }

        public async Task<IActionResult> AddRecete()
        {
            var result = await _malzemeService.GetAllMalzemeAsync();
            return View(result);
        }

        //Kullanıcı istedigi receteyi devreye almasi icin Yazmis oldugum metot.
        [HttpPost]//Küçük bir olayı anlamadık gibi!!!
        public async Task<IActionResult> ActivateRecete(int receteBaslikId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var recete = await _context.ReceteBasliklar
                     .Where(r => r.ReceteBaslikId == receteBaslikId && r.IsActive == false)
                     .FirstOrDefaultAsync();

                    if (recete == null)
                    {
                        return NotFound(new { message = "Recete Bulunamadi" });
                    }

                    recete.IsActive = true;
                    _context.ReceteBasliklar.Update(recete);

                    var pasifReceteler = _context.ReceteBasliklar
                        .Where(r => r.MalzemeId == recete.MalzemeId && r.IsActive == true)
                        .ToList();


                    foreach (var eskirecete in pasifReceteler)
                    {
                        eskirecete.IsActive = false;
                        _context.ReceteBasliklar.Update(eskirecete);
                    }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok(new { message = "Reçete başarıyla aktif hale getirildi." });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, new { error = "Beklenmeyen bir hata oluştu.", detay = ex.Message });
                }

            }
        }

        public async Task<IActionResult> DectivateRecete(int receteBaslikId)
        {
            try
            {
                var recete = await _context.ReceteBasliklar
                    .Where(r => r.ReceteBaslikId == receteBaslikId && r.IsActive == true)
                    .FirstOrDefaultAsync();

                if (recete == null)
                {
                    return NotFound(new { message = "Aktif reçete bulunamadı." });
                }

                recete.IsActive = false;
                _context.ReceteBasliklar.Update(recete);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Reçete başarıyla pasif hale getirildi." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Beklenmeyen bir hata oluştu.", detay = ex.Message });
            }
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
            var recete = await _context.ReceteBasliklar
                .Include(rb => rb.ReceteKalem)
                .ThenInclude(rk => rk.Malzeme)
                .FirstOrDefaultAsync(rb => rb.ReceteBaslikId == id);

            if (recete == null)
            {
                return NotFound();
            }

            var model = new ReceteViewModel
            {
                ReceteIsmi = recete.ReceteIsmi,
                Aciklama = recete.Aciklama,
                VersiyonNo = recete.VersiyonNo,
                Kalemler = recete.ReceteKalem.Select(rk => new ReceteKalemViewModel
                {
                    ReceteKalemId = rk.ReceteKalemId,
                    MalzemeAdi = rk.Malzeme?.MalzemeAdi ?? "Bilinmeyen Malzeme",
                    MalzemeId = rk.MalzemeId,
                    Miktar = rk.Miktar
                }).ToList()
            };

            return View(model);
        }

     
        //Receteyi excell formatinda kullanıcının indirme işlemi
        public async Task<IActionResult> DownloadExcell(int id)
        {
            var recete = await _context.ReceteBasliklar
                .Include(rb => rb.ReceteKalem)
                .ThenInclude(rk => rk.Malzeme)
                .FirstOrDefaultAsync(rb => rb.ReceteBaslikId == id);

            if (recete == null)
            {
                return NotFound();
            }

            try
            {
                using (var workbook = new ClosedXML.Excel.XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Reçete Detayları");

                    // Başlık bilgileri
                    worksheet.Cell(1, 1).Value = "Reçete Adı:";
                    worksheet.Cell(1, 2).Value = recete.ReceteIsmi;

                    worksheet.Cell(2, 1).Value = "Versiyon No:";
                    worksheet.Cell(2, 2).Value = recete.VersiyonNo;

                    worksheet.Cell(3, 1).Value = "Açıklama:";
                    worksheet.Cell(3, 2).Value = recete.Aciklama;

                    // Malzeme detayları başlık
                    worksheet.Cell(5, 1).Value = "Malzeme Adı";
                    worksheet.Cell(5, 2).Value = "Malzeme ID";
                    worksheet.Cell(5, 3).Value = "Miktar";

                    // Malzeme detayları doldurma
                    int currentRow = 6;
                    foreach (var kalem in recete.ReceteKalem)
                    {
                        worksheet.Cell(currentRow, 1).Value = kalem.Malzeme?.MalzemeAdi ?? "Bilinmeyen Malzeme";
                        worksheet.Cell(currentRow, 2).Value = kalem.MalzemeId;
                        worksheet.Cell(currentRow, 3).Value = kalem.Miktar;
                        currentRow++;
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        stream.Seek(0, SeekOrigin.Begin);

                        return File(stream.ToArray(),
                                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                    $"{recete.ReceteIsmi}_Detaylari.xlsx");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Excel oluşturulurken bir hata oluştu: {ex.Message}");
            }
        }


    }
}