using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Entity.Concrete
{
    public class Malzeme
    {
        public int Id { get; set; }
        public string MalzemeAdi { get; set; }

        public bool IsActive { get; set; }

        public int MalzemeGrupId { get; set; } // Foreign Key
        public MalzemeGrup MalzemeGrup { get; set; } // Navigation Property

        public decimal StokMiktari { get; set; }

        public int MalzemeBirimId { get; set; } // Foreign Key
        public MalzemeBirim MalzemeBirim { get; set; } // Navigation Property

        public List<Fiyat> MalzemeFiyatlar { get; set; } // One-to-Many ilişki

        public ICollection<ReceteKalem> ReceteKalems { get; set; }
        public ICollection<Stok> Stoklar { get; set; }
    }   
}

