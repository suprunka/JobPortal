﻿ using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories
{
    public interface IRepository<T> where T : class
    {
        bool Create(T obj);
        T Get(Expression<Func<T, bool>> predicate);
        bool Update(T obj);
        bool Delete(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        IQueryable<T> List(Expression<Func<T, bool>> predicate);

    }
}