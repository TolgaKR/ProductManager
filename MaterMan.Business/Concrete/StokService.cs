using MaterMan.Business.Abstract;
using MaterMan.Data.Abstract;
using MaterMan.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Business.Concrete
{
    public class StokService : IStokService
    {
        private readonly IStokDal _stokDal;

        public StokService(IStokDal stokDal)
        {
            _stokDal = stokDal;
        }

        public async Task AddStokAsync(Stok stok)
        {
            await _stokDal.AddAsync(stok);
        }

        public Task DeleteStokAsync(int stokId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Stok>> GetAllStokAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Stok> GetByStokIdAsync(int stokId)
        {
            throw new NotImplementedException();
        }

        public Task<Stok> GetStokByMalzemeIdAsync(int malzemeId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStokAsync(Stok stok)
        {
            throw new NotImplementedException();
        }
    }
}
