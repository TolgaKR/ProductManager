using MaterMan.Entity.Concrete;
using System.ComponentModel.DataAnnotations;  // Bu satırı ekle

namespace MaterMan.Models
{
    public class MaterialViewModel
    {


        // Malzeme Id (Varsa güncelleme veya detay için kullanılır)
        public int Id { get; set; }

        // Malzeme adı (Zorunlu alan)
        [Required(ErrorMessage = "Malzeme adı zorunludur.")]
        public string MalzemeAdi { get; set; }

        // Malzeme Grubu ID (Seçilen grubu belirtmek için)
      [Required(ErrorMessage = "Malzeme grubu seçilmelidir.")]
        public int MalzemeGrupId { get; set; }

        // Malzeme Grubu Adı (Bu, View'de görüntülenebilir)
        public string? MalzemeGrupAdi { get; set; }

        //Malzeme Birimi ID (Seçilen birimi belirtmek için)
       [Required(ErrorMessage = "Malzeme birimi seçilmelidir.")]
        public int MalzemeBirimId { get; set; }

        // Malzeme Fiyatı (Zorunlu ve geçerli aralıkta olması gerekir)
        [Required(ErrorMessage = "Fiyat zorunludur.")]
        public decimal Fiyat { get; set; }

        // Stok Miktarı (Varsa mevcut stok)
        public decimal StokMiktari { get; set; }
       

    }
}
