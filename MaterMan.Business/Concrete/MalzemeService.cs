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
    public class MalzemeService : IMalzemeService
    {

        private readonly IMalzemeDal _malzemeDal;


        public MalzemeService(IMalzemeDal malzemeDal)
        {
            _malzemeDal = malzemeDal;
        }

        public void Add(Malzeme entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddMalzemeAsync(Malzeme malzeme)
        {
            if (malzeme == null)
            {

                throw new ArgumentNullException(nameof(malzeme), "Malzeme nesnesi boş olamaz. Lütfen geçerli bir malzeme nesnesi sağlayın.");

            }

            await _malzemeDal.AddAsync(malzeme);
        }

        public void Delete(Malzeme entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteMalzemeAsync(int malzemeId)
        {
            await _malzemeDal.MalzemeDeleteAsync(malzemeId);

        }

        public List<Malzeme> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Malzeme>> GetAllMalzemeAsync()
        {
            return await _malzemeDal.MalzemeList();
        }

        public async Task<Malzeme> GetById(int id)
        {
            return await _malzemeDal.GetByIdAsync(id);
        }

        public async Task<Malzeme> GetByIdAs(int id)
        {
            return await _malzemeDal.GetByIdAsync(id);
        }

        public Task<Malzeme> GetByMalzemeIdAsync(int malzemeId)
        {
            throw new NotImplementedException();
        }

        public void Update(Malzeme entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateMalzemeAsync(Malzeme malzeme)
        {
           await _malzemeDal.UpdateAsync(malzeme);
            
        }

        Malzeme Abstract.IRepository<Malzeme>.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
