using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebCrawler.Api.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));
        T Update(T entity);
        Task<T> UpdateAsync(long id, T entity, CancellationToken cancellationToken = default(CancellationToken));
        T Delete(long id);
        T Delete(T entity);
        Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));
        T GetById(long id);
        Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null, CancellationToken cancellationToken = default(CancellationToken));        
        bool Save();
        Task<bool> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
