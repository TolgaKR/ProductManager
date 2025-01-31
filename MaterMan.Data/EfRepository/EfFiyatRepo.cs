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
    public class EfFiyatRepo: GenericRepository<Fiyat>, IFiyatDal
    {
        public EfFiyatRepo(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
