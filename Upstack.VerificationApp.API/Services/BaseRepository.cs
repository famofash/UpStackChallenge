using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upstack.VerificationApp.API.Contracts;
using Upstack.VerificationApp.API.Entities;

namespace Upstack.VerificationApp.API.Services
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        protected APIDbContext _dbContext { get; set; }

        public async Task<T> GetAsync(int id) => await _dbContext.FindAsync<T>(id);

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>().AsQueryable();
        }
        public IQueryable<T> QueryWithOptions(Func<T, bool> where)
        {
            return _dbContext.Set<T>().Where(where).AsQueryable();
        }

        public async Task InsertAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
