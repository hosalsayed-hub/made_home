using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.DAL.Contract
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        T GetById(object id);
        T Insert(T entity);
        T Update(T entity);
        void delete(object id);
        void Delete(T entity);
        void DeleteRang(IEnumerable<T> listEntity);
        void UpdateRang(IEnumerable<T> listEntity);
    }
}
