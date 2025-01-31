using MaterMan.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Business.Abstract
{
    public interface IReceteKalemService
    {
        Task<List<ReceteKalem>> GetAllReceteKalemAsync();

        Task AddReceteKalemAsync(ReceteKalem receteKalem);
        Task DeleteReceteKalemAsync(int receteKalemId);

        Task UpdateReceteKalemAsync(ReceteKalem receteKalem);

        Task<ReceteKalem> GetByReceteKalemIdAsync(int receteKalemId);
    }
}
