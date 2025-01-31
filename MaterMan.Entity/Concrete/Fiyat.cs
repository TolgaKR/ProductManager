using MaterMan.Entity.Concrete;

public class Fiyat
{
    public int Id { get; set; }
    public int MalzemeId { get; set; }
    public Malzeme Malzeme { get; set; }  // Navigation Property

    public decimal GuncelFiyat { get; set; }
    public decimal EskiFiyat { get; set; }
    public DateTime EskiFiyatTarih { get; set; }
    public DateTime YeniFiyatTarih { get; set; }
}
