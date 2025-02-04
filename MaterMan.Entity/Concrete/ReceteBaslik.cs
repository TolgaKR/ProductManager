using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;           //Her reçetekalemin bir başlığı olcak

namespace MaterMan.Entity.Concrete
{
    public class ReceteBaslik
    {
        public int ReceteBaslikId { get; set; }
        public string ReceteIsmi { get; set; }
        public string Aciklama { get; set; }
        public DateTime EklemeTarihi { get; set; }
        public bool IsActive { get; set; }
        public int MalzemeId { get; set; }
        public int VersiyonNo { get; set; }

        
        public List<ReceteKalem> ReceteKalem { get; set; }


    }
}
