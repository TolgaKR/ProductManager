using MaterMan.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Entity.Concrete
{
    public class MalzemeGrup
    {
        public int Id { get; set; }
        public string GrupAdi { get; set; }
        public ICollection<Malzeme> Malzemeler { get; set; } // One-to-Many ilişki

    }
}
