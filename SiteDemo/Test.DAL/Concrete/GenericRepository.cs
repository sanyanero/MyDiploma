using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test.DAL.Abstract;
using Test.MODELS;
using Test.MODELS.Entities;

namespace Test.DAL.Concrete
{
    ///
    /// Not all of these methods are being used but its my GenericRepository which i use in my projects
    /// 
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly ApplicationContext _context;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual IQueryable<T> Data => _dbSet;

        public virtual IQueryable<T> GetAsync(List<Expression<Func<T, bool>>> filters = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            return query;
        }

        public virtual async Task<List<T>> GetPagedAsync(List<Expression<Func<T, bool>>> filters = null, Expression<Func<T, object>> orderBy = null, int count = 10, int page = 0, params Expression<Func<T, object>>[] includes)
        {
            if (orderBy != null)
            {
                return await GetAsync(filters, includes)
                    .OrderBy(orderBy)
                    .Skip(page * count)
                    .Take(count)
                    .ToListAsync();
            }
            else
            {
                return await GetAsync(filters, includes)
                    .Skip(page * count)
                    .Take(count)
                    .ToListAsync();
            }
        }


        public virtual Task<List<T>> GetPagedAsync(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderBy = null, int count = 10, int page = 0, params Expression<Func<T, object>>[] includes)
        {
            return GetPagedAsync(filters: filter != null ? new List<Expression<Func<T, bool>>> { filter } : null, orderBy: orderBy, count: count, page: page, includes: includes);
        }

        public virtual async Task<T> FirstOrDefaultAsync(List<Expression<Func<T, bool>>> filters = null, params Expression<Func<T, object>>[] includes)
        {
            return await GetAsync(filters, includes)
                .FirstOrDefaultAsync();
        }

        public virtual Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            return FirstOrDefaultAsync(filters: filter != null ? new List<Expression<Func<T, bool>>> { filter } : null, includes: includes);
        }

        public virtual async Task<int> CountAsync(List<Expression<Func<T, bool>>> filters = null)
        {
            return await GetAsync(filters)
                .CountAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
        {
            return await GetAsync(filters: filter != null ? new List<Expression<Func<T, bool>>> { filter } : null)
                .CountAsync();
        }

        public virtual Task<T> GetByIdAsync(object id)
        {
            return _dbSet.FindAsync(id);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            return (await _dbSet.AddAsync(entity)).Entity;
        }


        public virtual Task AddRangeAsync(IEnumerable<T> entities)
        {
            return _dbSet.AddRangeAsync(entities);
        }

        public virtual void Delete(T entity)
        {

            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual T Update(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual async Task<bool> Exist(Guid key)
        {
            return await _dbSet.AnyAsync(o => o.Id == key);
        }

    }
}
