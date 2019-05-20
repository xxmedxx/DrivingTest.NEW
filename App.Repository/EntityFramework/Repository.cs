using App.DataBaseAccess;
using App.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace App.EntityFramework.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        readonly DrivingTestDBEntities DB;
        readonly DbSet table;
        public Repository(DrivingTestDBEntities DB)
        {
            this.DB = DB;
            table = DB.Set<T>();
        }

        public int Delete(object id)
        {
            var obj = table.Find(id);
            if (table.Remove(obj) != null)
                return 1;
            else
                return 0;
        }
        public Task<int> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetQueryableSet(bool includAll = false)
        {
            return (IQueryable<T>)table;
        }
        public IEnumerable<T> GetAll(bool includAll = false)
        {            
            var lst = ((IEnumerable<T>) table).ToList<T>();
            return  lst;
        }
        public Task<IEnumerable<T>> GetAllAsync(bool includAll = false)
        {
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            var obj = table.Find(id);
            if (obj == null)
                return null;
            return (T)obj;
        }
        public Task<T> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public T Insert(T obj)
        {
            try
            {
               return (T)table.Add(obj);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Task<T> InsertAsync(T obj)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            return DB.SaveChanges();
        }
        public Task<int> SaveAsync()
        {
            return DB.SaveChangesAsync();
        }

        public T Update(T obj)
        { 
            try 
	        {
                table.Attach(obj);
                DB.Entry(obj).State = EntityState.Modified;
                return obj;
            }
	        catch (Exception)
	        {
                return null;
	        }            
         }
        public Task<T> UpdateAsync(T obj)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            if (DB != null)
            {
                DB.Dispose();
            }

            GC.SuppressFinalize(this);
        }

       
    }
}
