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
    public class EfMalzemeGrupRepo: GenericRepository<MalzemeGrup>, IMalzemeGrupDal
    {
        public EfMalzemeGrupRepo(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
