using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Basty.Interfaces;
using Basty.Models;

namespace Basty.Repository
{
   public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected BastyDBContext bastyDBContext { get; set; }
 
        public RepositoryBase(BastyDBContext bastyDBContext)
        {
            this.bastyDBContext = bastyDBContext;
        }
 
        public IQueryable<T> IncludeRels(Expression<Func<T, T>> expression)
        {
            return this.bastyDBContext.Set<T>().Select(expression).AsNoTracking();
        }
        public IQueryable<T> SelectAll()
        {
            return this.bastyDBContext.Set<T>().Select(r=>r).AsNoTracking();
        }
 
        public T FindOne(Expression<Func<T, bool>> expression)
        {
            return this.bastyDBContext.Set<T>().SingleOrDefault(expression);
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.bastyDBContext.Set<T>().Where(expression).AsNoTracking();
        }
 
        public void Create(T entity)
        {
            this.bastyDBContext.Set<T>().Add(entity);
        }
 
        public void Update(T entity)
        {
            this.bastyDBContext.Set<T>().Update(entity);
        }
 
        public void Delete(T entity)
        {
            this.bastyDBContext.Set<T>().Remove(entity);
        }

        public void RemoveAllRange(Expression<Func<T, bool>> expression)
        {
            var list=this.bastyDBContext.Set<T>().Where(expression).AsNoTracking();
            this.bastyDBContext.Set<T>().RemoveRange(list);
        }
    }
}