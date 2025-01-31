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
    public class MalzemeBirimService : IMalzemeBirimService
    {
        private readonly IMalzemeBirimDal _malzemeBirimDal;

        public MalzemeBirimService(IMalzemeBirimDal malzemeBirimDal)
        {
            _malzemeBirimDal = malzemeBirimDal;
        }
        public Task<List<MalzemeBirim>> GetAllMalzemeBirimAsync()
        {
            return _malzemeBirimDal.GetAllAsync();
        }
    }
}
