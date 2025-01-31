using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Data.Abstract
{
    public interface IRepository<T> where T:class
    {
        // Asenkron metodlar
        Task AddAsync(T entity);  // Her tür entity için geçerli
        Task UpdateAsync(T entity); // Her tür entity için geçerli
        Task DeleteAsync(int id); // ID'ye göre silme işlemi
        Task<T> GetByIdAsync(int id);  // ID'ye göre alma işlemi
        Task<List<T>> GetAllAsync();  // Tüm kayıtları alma işlemi
    }
}
