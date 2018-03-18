using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Test.DAL.Abstract;
using Test.MODELS;
using Test.MODELS.Entities;
using Test.MODELS.Helpers;

namespace Test.DAL.Concrete
{
    public class ProductsRepository : GenericRepository<Product>, IProductsRepository
    {
        public ProductsRepository(ApplicationContext context) : base(context) { }
        public IEnumerable<GroupModel<T>> GroupBy<T>(Expression<Func<Product, T>> groupBy, int page, int count)
        {
            return _dbSet.GroupBy(groupBy)
                .Skip(page * count).Take(count)
                .Select(g => new GroupModel<T> { Key = g.Key, Count = g.Count() })
                .ToList();

        }
        public int GroupCount<T>(Expression<Func<Product, T>> groupBy)
        {
            return _dbSet.GroupBy(groupBy)
                .Count();
        }
    }
}
