using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Entity.Concrete
{
    public class Stok
    {
        public int StokId { get; set; }

        public int MalzemeId { get; set; }  // Foreign Key for Malzeme

        // Navigation property for Malzeme
        public Malzeme? Malzeme { get; set; }
        public int StokAdet { get; set; }

        public string IslemTipi { get; set; } //Çıkış, Giriş

        public DateTime IslemTarihi { get; set; }
    }
}
