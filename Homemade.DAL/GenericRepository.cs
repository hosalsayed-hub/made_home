using Homemade.DAL.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HomemadeContext _context;
        private DbSet<T> table = null;
        public GenericRepository(HomemadeContext context)
        {
            _context = context;
            table = context.Set<T>();
        }
        public void delete(object id)
        {
            T existing = GetById(id);
            table.Remove(existing);
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = table.AsQueryable();
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
              .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query;
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = table.AsQueryable();
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
              .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }

        public T Insert(T entity)
        {
            table.Add(entity);
            return entity;
        }
        public T Update(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        public void Delete(T TEntity)
        {
            table.Remove(TEntity);
        }
        public void DeleteRang(IEnumerable<T> listEntity)
        {
            table.RemoveRange(listEntity);
        }
        public void UpdateRang(IEnumerable<T> listEntity)
        {
            listEntity.ToList().ForEach(e =>
            {
                _context.Entry(e).State = EntityState.Modified;
            });
        }
    }
}
