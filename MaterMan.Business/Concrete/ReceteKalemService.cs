using MaterMan.Business.Abstract;
using MaterMan.Data.Abstract;
using MaterMan.Entity.Concrete;
using MaterMan.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Business.Concrete
{
    public class ReceteKalemService : IReceteKalemService
    {
        private IReceteBaslikDal _receteBaslikDal;


        public ReceteKalemService(IReceteBaslikDal receteBaslikDal)
        {
            _receteBaslikDal = receteBaslikDal;
        }

        public Task AddReceteKalemAsync(ReceteKalem receteKalem)
        {
            throw new NotImplementedException();
        }

        public Task DeleteReceteKalemAsync(int receteKalemId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReceteKalem>> GetAllReceteKalemAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReceteKalem> GetByReceteKalemIdAsync(int receteKalemId)
        {
            throw new NotImplementedException();
        }

        public Task<ReceteDetayViewModel> GetReceteDetayAsync(int id)
        {
           return _receteBaslikDal.GetReceteDetay(id);
        }

        public Task UpdateReceteKalemAsync(ReceteKalem receteKalem)
        {
            throw new NotImplementedException();
        }
    }
}
