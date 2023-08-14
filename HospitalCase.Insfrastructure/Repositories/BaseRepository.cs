using HospitalCase.Application.Interfaces;
using HospitalCase.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalCase.Insfrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<int, TEntity> where TEntity : DomainObject, new()
    {
        protected readonly HospitalCaseDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(HospitalCaseDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
                var result = await _dbSet.AddAsync(entity);

                await _dbContext.SaveChangesAsync();

                return result.Entity;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            TEntity found = await _dbSet.FirstOrDefaultAsync((e) => e.Id == id);

            if(found == null)
            {
                return false;
            }

            _dbSet.Remove(found);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync((e) => e.Id == id);
        }

        public virtual async Task<TEntity> UpdateAsync(int id, TEntity entity)
        {
            var entry = await _dbSet.FindAsync(id);

            if (entry != null)
            {
                _dbContext.Entry(entry).State = EntityState.Detached;
            }

            _dbSet.Attach(entity);

            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
