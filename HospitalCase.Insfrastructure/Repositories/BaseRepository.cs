using HospitalCase.Application.Interfaces;
using HospitalCase.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalCase.Insfrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<int, TEntity> where TEntity : DomainObject, new()
    {
        protected readonly HospitalCaseDbContextFactory _dbContextFactory;

        protected BaseRepository(HospitalCaseDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            using(var dbContext = _dbContextFactory.CreateDbContext())
            {
                var result = await dbContext.Set<TEntity>().AddAsync(entity);
                await dbContext.SaveChangesAsync();

                return result.Entity;
            }
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                TEntity found = await dbContext.Set<TEntity>().FirstOrDefaultAsync((e) => e.Id == id);
                dbContext.Set<TEntity>().Remove(found);
                await dbContext.SaveChangesAsync();

                return true;
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<TEntity> found = await dbContext.Set<TEntity>().ToListAsync();

                return found;
            }
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                TEntity found = await dbContext.Set<TEntity>().FirstOrDefaultAsync((e) => e.Id == id);

                return found;
            }
        }

        public virtual async Task<TEntity> UpdateAsync(int id, TEntity entity)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                dbContext.Set<TEntity>().Update(entity);
                await dbContext.SaveChangesAsync();

                return entity;
            }
        }
    }
}
