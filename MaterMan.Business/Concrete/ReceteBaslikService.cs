using MaterMan.Business.Abstract;
using MaterMan.Data.Abstract;
using MaterMan.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Business.Concrete
{
    public class ReceteBaslikService : IReceteBaslikService
    {

        private IReceteBaslikDal _receteBaslikDal;

        public ReceteBaslikService(IReceteBaslikDal receteBaslikDal)
        {
            _receteBaslikDal = receteBaslikDal;
        }


        public async Task<bool> AcceptReceteBaslikAsync(int id)
        {
            var result=  await _receteBaslikDal.AcceptReceteBaslik(id);
            return result;
        }

        public Task AddReceteBaslikAsync(ReceteBaslik receteBaslik)
        {
            throw new NotImplementedException();
        }

        public Task DeleteReceteBaslikAsync(int receteBaslikId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReceteBaslik>> GetAllReceteBaslikAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReceteBaslik> GetByReceteBaslikIdAsync(int receteBaslikId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateReceteBaslikAsync(ReceteBaslik receteBaslik)
        {
            throw new NotImplementedException();
        }
    }
}
