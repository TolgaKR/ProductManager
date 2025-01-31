using MaterMan.Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Data.Concrete
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _appDbcontext;
        private readonly DbSet<T> _appDbSet;



        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbcontext = appDbContext;
            _appDbSet = appDbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
           await _appDbcontext.AddAsync(entity);
              await _appDbcontext.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _appDbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _appDbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _appDbcontext.Update(entity);
            await _appDbcontext.SaveChangesAsync();
        }
    }
}
