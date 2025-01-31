using MaterMan.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Data.Abstract
{
    public interface IMalzemeDal : IRepository<Malzeme>
    {
        Task MalzemeDeleteAsync(int malzemeId);
        
        


        Task<List<Malzeme>> MalzemeList();


    }
}
