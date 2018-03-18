using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Test.MODELS.Entities;
using Test.MODELS.Helpers;

namespace Test.DAL.Abstract
{
    public interface IProductsRepository : IGenericRepository<Product>
    {
        IEnumerable<GroupModel<T>> GroupBy<T>(Expression<Func<Product, T>> groupBy, int page, int count);
        int GroupCount<T>(Expression<Func<Product, T>> groupBy);
    }
}
