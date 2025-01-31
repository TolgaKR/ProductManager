using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Entity.Concrete
{
    public class UrunTipi
    {
        public int Id { get; set; } // Birincil anahtar.
        public string UrunAdi { get; set; } // Ürün tipinin adı (örneğin, Nihai Ürün, Yan Ürün, Hammadde).
        
    }
}
