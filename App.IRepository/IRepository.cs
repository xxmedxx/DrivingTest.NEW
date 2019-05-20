using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.IRepository
{
    public interface IRepository<T>:IDisposable where T : class
    {
        IEnumerable<T> GetAll(bool includAll = false);
        IQueryable<T> GetQueryableSet(bool includAll = false);
        Task<IEnumerable<T>> GetAllAsync(bool includAll = false);

        T GetById(object id);
        Task<T> GetByIdAsync(object id);

        T Insert(T obj);
        Task<T> InsertAsync(T obj);

        T Update(T obj);
        Task<T> UpdateAsync(T obj);

        int Delete(object id);
        Task<int> DeleteAsync(object id);

        int Save();
        Task<int> SaveAsync();
    }
}
