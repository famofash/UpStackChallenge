using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upstack.VerificationApp.API.Contracts
{
    public interface IBaseRepository<T>
    {
        Task<T> GetAsync(int id);

        IQueryable<T> Query();
        IQueryable<T> QueryWithOptions(Func<T, bool> where);

        Task InsertAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task Commit();
    }
}
