using MaterMan.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Business.Abstract
{
    public interface IFiyatService
    {
        Task<List<Fiyat>> GetAllFiyatAsync();
        Task<Fiyat> GetByFiyatIdAsync(int fiyatId);

        Task AddFiyatIdAsync(Fiyat fiyat);
        Task UpdateFiyatAsync(Fiyat fiyat);
        Task DeleteFiyatAsync(int fiyatId);

    }
}
