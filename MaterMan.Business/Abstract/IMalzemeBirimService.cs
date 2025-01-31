using MaterMan.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Business.Abstract
{
    public interface IMalzemeBirimService 
    {

        Task<List<MalzemeBirim>> GetAllMalzemeBirimAsync();
        

    }
}
