using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebCrawler.Api.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
            return entities;
        }
        public T Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _dbSet.Update(entity);
            return entity;
        }
        public async Task<T> UpdateAsync(long id, T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var entityToUpdate = await _dbSet.FindAsync(id);

            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).State = EntityState.Modified;
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                _dbSet.Update(entity);
            }

            return entityToUpdate;
        }
        public T Delete(long id)
        {
            T entityToDelete = _dbSet.Find(id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
            return entityToDelete;
        }
        public T Delete(T entity)
        {
            _dbSet.Remove(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
            return entities;
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
            return entities;
        }

        public T GetById(long id)
        {
            var entity = _dbSet.Find(id);
            return entity;
        }

        public async Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await _dbSet.FindAsync(new object[] { id }, cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync(cancellationToken);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }       

        public bool Save()
        {
            int result;
            try
            {
                result = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return result > 0 ? true : false;
        }

        public async Task<bool> SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            int result;
            try
            {
                result = await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return result > 0 ? true : false;
        }        
    }
}
