using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace Basty.Interfaces
{
    public interface IRepositoryBase<T>
    {
        T FindOne(Expression<Func<T, bool>> expression);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        IQueryable<T> SelectAll();
        IQueryable<T> IncludeRels(Expression<Func<T, T>> expression);

        void RemoveAllRange(Expression<Func<T, bool>> expression);
        
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}