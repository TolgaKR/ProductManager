using MaterMan.Entity.Concrete;
using MaterMan.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Data.Abstract
{
    public interface IReceteBaslikDal:IRepository<ReceteBaslik>
    {
        public Task<ReceteDetayViewModel> GetReceteDetay(int id);

        public Task<bool> AcceptReceteBaslik(int id);
        

    }
}
