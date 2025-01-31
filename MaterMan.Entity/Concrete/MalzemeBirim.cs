using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Entity.Concrete
{
    public class MalzemeBirim
    {
        public int Id { get; set; }
        public string BirimAdi { get; set; } // Örn: "Kg", "Metre", "Adet"

        public ICollection<Malzeme> Malzemeler { get; set; } // One-to-Many ilişki

    }
}
