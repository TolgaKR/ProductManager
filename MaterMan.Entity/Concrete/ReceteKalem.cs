using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Entity.Concrete
{
    public class ReceteKalem
    {
        public int ReceteKalemId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Miktar { get; set; }

        // Foreign Key for ReceteBaslik
        public int ReceteBaslikId { get; set; }

        // Navigation Property for ReceteBaslik
        public ReceteBaslik ReceteBaslik { get; set; }

        // Foreign Key for Malzeme (İlişkiyi bağlamak)
        public int MalzemeId { get; set; }

        // Navigation Property for Malzeme
        public Malzeme Malzeme { get; set; }
        
    }
}
