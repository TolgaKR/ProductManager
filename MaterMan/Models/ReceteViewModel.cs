namespace MaterMan.Models
{
    public class ReceteViewModel
    {
        public int ReceteBaslikId { get; set; }
        public string ReceteIsmi { get; set; }
        public string Aciklama { get; set; }
        public int VersiyonNo { get; set; }
        public List<ReceteKalemViewModel> Kalemler { get; set; }

        public int MalzemeId { get; set; }
        public string MalzemeAdi { get; set; } // MalzemeAdi eklendi.
    }

    public class ReceteKalemViewModel
    {
        public int ReceteKalemId { get; set; }
        public int MalzemeId { get; set; }
        public string MalzemeAdi { get; set; } // MalzemeAdi eklendi.

        public decimal Miktar { get; set; }
    }



}
