using MaterMan.Data.Abstract;
using MaterMan.Data.Concrete;
using MaterMan.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Data.EfRepository
{
    public class EfStokRepo: GenericRepository<Stok>,  IStokDal

    {
        private readonly AppDbContext _appDbContext;

        public EfStokRepo(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Stok> GetByStokIdAsync(int stokId)
        {
            return await _appDbContext.Stoklar.FindAsync(stokId);
        }

        public async Task<Stok> GetStokByMalzemeIdAsync(int malzemeId)
        {
            return await _appDbContext.Stoklar.FirstOrDefaultAsync(s => s.MalzemeId == malzemeId);
        }
    }
}
