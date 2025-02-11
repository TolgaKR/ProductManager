using MaterMan.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Entity.ViewModel
{
    public class ReceteDetayViewModel
    {
        public List<ReceteKalem> receteKalem { get; set; } = new List<ReceteKalem>(); // Listeyi başlatıyoruz
        public List<ReceteBaslik> receteBaslik { get; set; } = new List<ReceteBaslik>(); // Listeyi başlatıyoruz
    }

}
