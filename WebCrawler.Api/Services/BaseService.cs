using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebCrawler.Api.Repository;

namespace WebCrawler.Api.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            var entityToAdd = await _repository.AddAsync(entity, cancellationToken);
            var result = await _repository.SaveAsync(cancellationToken);
            return result;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            var entitiesToAdd = await _repository.AddRangeAsync(entities, cancellationToken);
            var result = await _repository.SaveAsync(cancellationToken);
            return result;
        }

        public bool Update(T entity)
        {
            var entityToUpdate = _repository.Update(entity);
            var result = _repository.Save();
            return result;
        }

        public async Task<bool> UpdateAsync(long id, T entity, CancellationToken cancellationToken = default)
        {
            var entityToUpdate = await _repository.UpdateAsync(id, entity, cancellationToken);
            var result = await _repository.SaveAsync(cancellationToken);
            return result;
        }

        public bool Delete(long id)
        {
            var entityToDelete = _repository.Delete(id);
            var result = _repository.Save();
            return result;
        }

        public bool Delete(T entity)
        {
            var entityToDelete = _repository.Delete(entity);
            var result = _repository.Save();
            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, CancellationToken cancellationToken = default)
        {
            var result = await _repository.GetAllAsync(filter, orderBy, includeProperties, cancellationToken);
            return result;
        }
        public T GetById(long id)
        {
            var result = _repository.GetById(id);
            return result;
        }

        public async Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var result = await _repository.GetByIdAsync(id, cancellationToken);
            return result;
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null, CancellationToken cancellationToken = default)
        {
            var result = await _repository.GetFirstOrDefaultAsync(filter, includeProperties, cancellationToken);
            return result;
        }
        
    }
}
