using Microsoft.EntityFrameworkCore;
using Pubinno.Application.Interfaces.Repositories;
using Pubinno.Domain.Entities;
using Pubinno.Persistence.Context;
using System.Linq.Expressions;

namespace Pubinno.Persistence.Repositories
{
    public class EfRepositoryBase<TEntity> : IRepository<TEntity>
      where TEntity : BaseEntity
    {
        protected readonly PubinnoDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public EfRepositoryBase(PubinnoDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
            => await _dbSet.FindAsync(id);

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
            => await _dbSet.FirstOrDefaultAsync(predicate);

        public async Task<List<TEntity>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
            => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(TEntity entity)
            => await _dbSet.AddAsync(entity);

        public async Task UpdateAsync(TEntity entity)
            =>  _dbSet.Update(entity);

        public async Task DeleteAsync(TEntity entity)
            => _dbSet.Remove(entity); // soft delete AppDbContext'te hallediliyor
    }
}
