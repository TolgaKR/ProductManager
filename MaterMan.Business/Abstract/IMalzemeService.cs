using MaterMan.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Business.Abstract
{
    public interface IMalzemeService: IRepository<Malzeme>
    {

        Task<Malzeme> GetByIdAs(int id);
        Task AddMalzemeAsync(Malzeme malzeme);
        Task UpdateMalzemeAsync(Malzeme malzeme);
        Task DeleteMalzemeAsync(int malzemeId);

        Task<Malzeme> GetByMalzemeIdAsync(int malzemeId);
        Task<List<Malzeme>> GetAllMalzemeAsync();
        //Task<List<MalzemeGrup>> GetMalzemeGruplari();
    }
}
