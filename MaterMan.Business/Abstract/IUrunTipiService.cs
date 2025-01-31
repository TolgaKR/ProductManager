using MaterMan.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Business.Abstract
{
    public interface IUrunTipiService
    {

        Task<List<UrunTipi>> GetAllUrunTablosuAsync();
        Task<UrunTipi> GetByUrunTipiTablosuId();

        Task AddUrunTipiTablosuAsync(UrunTipi urunTipiTablosu);
        Task UpdateUrunTipiTablosuAsync(UrunTipi urunTipiTablosu);
        Task DeleteUrunTipiTablosuAsync(int urunTipiId);
    }
}
