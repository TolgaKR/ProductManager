using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Entity.Concrete
{
    public class Stok
    {
        public int StokId { get; set; }

        [ForeignKey("MalzemeId")] // Foreign Key
        public int MalzemeId { get; set; }  // Foreign Key for Malzeme

        // Navigation property for Malzeme
        public Malzeme? Malzeme { get; set; }
        public decimal StokAdet { get; set; }

        public string IslemTipi { get; set; } //Çıkış, Giriş

        public DateTime IslemTarihi { get; set; }
    }
}
