using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Test.DAL.Abstract
{
    public interface IGenericRepository<T> where T : class {
        IQueryable<T> Data { get; }

        IQueryable<T> GetAsync(List<Expression<Func<T, bool>>> filter = null, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetPagedAsync(List<Expression<Func<T, bool>>> filters = null, Expression<Func<T, object>> orderBy = null, int count = 10, int page = 0, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetPagedAsync(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderBy = null, int count = 10, int page = 0, params Expression<Func<T, object>>[] includes);
        Task<int> CountAsync(List<Expression<Func<T, bool>>> filters = null);
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);
        Task<T> GetByIdAsync(object id);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Delete(T entity);
        T Update(T entity);
        Task<bool> Exist(Guid id);
    }
}
