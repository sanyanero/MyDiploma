using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Test.MODELS.Entities;
using Test.MODELS.Helpers;

namespace Test.DAL.Abstract
{
    public interface IQuestionsRepository : IGenericRepository<Question>
    {
        IEnumerable<GroupModel<T>> GroupBy<T>(Expression<Func<Question, T>> groupBy, int page, int count);
        int GroupCount<T>(Expression<Func<Question, T>> groupBy);
    }
}
