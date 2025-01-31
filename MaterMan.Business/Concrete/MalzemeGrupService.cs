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
    public class MalzemeGrupService : IMalzemeGrupService
    {

        private readonly IMalzemeGrupDal _malzemeGrupDal;

        public MalzemeGrupService(IMalzemeGrupDal malzemeGrupDal)
        {
            _malzemeGrupDal = malzemeGrupDal;
        }
        public Task AddMalzemeGrupAsync(MalzemeGrup malzemeGrup)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMalzemeGrupAsync(int malzemeGrupId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MalzemeGrup>> GetAllMalzemeGrupAsync()
        {
            var result = await _malzemeGrupDal.GetAllAsync(); // Eksik await eklendi
            return result ?? new List<MalzemeGrup>();
        }

        public Task<MalzemeGrup> GetByMalzemeGrupIdAsync(int malzemeGrupId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMalzemeGrupAsync(MalzemeGrup malzemeGrup)
        {
            throw new NotImplementedException();
        }
    }
}
