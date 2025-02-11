using MaterMan.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Business.Abstract
{
    public interface IReceteBaslikService
    {
        Task<List<ReceteBaslik>> GetAllReceteBaslikAsync();
        Task AddReceteBaslikAsync(ReceteBaslik receteBaslik);
        Task UpdateReceteBaslikAsync(ReceteBaslik receteBaslik);

        Task<bool> AcceptReceteBaslikAsync(int id);

        Task DeleteReceteBaslikAsync(int receteBaslikId);
        Task<ReceteBaslik> GetByReceteBaslikIdAsync(int receteBaslikId);
    }
}
