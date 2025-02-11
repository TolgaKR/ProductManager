using MaterMan.Business.Abstract;
using MaterMan.Data.Abstract;
using MaterMan.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteStokAsync(int stokId)
        {
             await _stokDal.GetStokByMalzemeIdAsync(stokId);
        }

        public Task<List<Stok>> GetAllStokAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Stok> GetByStokIdAsync(int stokId)
        {
            throw new NotImplementedException();
        }

        public async Task<Stok> GetStokByMalzemeIdAsync(int malzemeId)
        {
           return await _stokDal.GetStokByMalzemeIdAsync(malzemeId);
        }

        public async Task UpdateStokAsync(Stok stok)
        {
          await  _stokDal.UpdateAsync(stok);
        }
    }
}
