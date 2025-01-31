using MaterMan.Data.Abstract;
using MaterMan.Data.Concrete;
using MaterMan.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Data.EfRepository
{
    public class EfSonUrunRepo:GenericRepository<SonUrun>, ISonUrunDal
    {
        public EfSonUrunRepo(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public Task AddAsync(ISonUrunDal entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ISonUrunDal entity)
        {
            throw new NotImplementedException();
        }

        Task<List<ISonUrunDal>> IRepository<ISonUrunDal>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<ISonUrunDal> IRepository<ISonUrunDal>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
