using MaterMan.Business.Abstract;
using MaterMan.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Business.Concrete
{
    public class FiyatService : IFiyatService
    {
        public Task AddFiyatIdAsync(Fiyat fiyat)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFiyatAsync(int fiyatId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fiyat>> GetAllFiyatAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Fiyat> GetByFiyatIdAsync(int fiyatId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFiyatAsync(Fiyat fiyat)
        {
            throw new NotImplementedException();
        }
    }
}
