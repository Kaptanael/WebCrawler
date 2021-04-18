using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebCrawler.Api.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<bool> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));
        bool Update(T entity);
        Task<bool> UpdateAsync(long id, T entity, CancellationToken cancellationToken = default(CancellationToken));
        bool Delete(long id);
        bool Delete(T entity);
        T GetById(long id);
        Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null, CancellationToken cancellationToken = default(CancellationToken));        
    }
}
