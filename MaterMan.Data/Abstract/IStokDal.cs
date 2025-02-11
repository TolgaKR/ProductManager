using MaterMan.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Data.Abstract
{
    public interface IStokDal:IRepository<Stok>
    {
        Task<Stok> GetByStokIdAsync(int stokId); // StokId'ye göre arama yapın
        Task<Stok> GetStokByMalzemeIdAsync(int malzemeId); // MalzemeId'ye göre arama (gerekirse)
    }
}
