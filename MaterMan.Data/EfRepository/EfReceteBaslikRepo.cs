using MaterMan.Data.Abstract;
using MaterMan.Data.Concrete;
using MaterMan.Entity.Concrete;
using MaterMan.Entity.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Data.EfRepository
{
    public class EfReceteBaslikRepo: GenericRepository<ReceteBaslik>,IReceteBaslikDal
    {

        private AppDbContext _appDbContext;

        
        public EfReceteBaslikRepo(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> AcceptReceteBaslik(int id)
        {
            var result = await _appDbContext.ReceteBasliklar.Where(x => x.ReceteBaslikId == id).FirstOrDefaultAsync();
            result.IsActive=true;
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ReceteDetayViewModel> GetReceteDetay(int id)
        {
            var result = await _appDbContext.ReceteKalemler.Where(x => x.ReceteBaslikId == id).Include(x => x.ReceteBaslik).Include(x => x.Malzeme).ToListAsync();
            ReceteDetayViewModel receteDetayViewModel = new ReceteDetayViewModel()
            {
                receteKalem = result
            };

            return receteDetayViewModel;

        }
    }
}
