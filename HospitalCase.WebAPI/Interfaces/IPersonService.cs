using HospitalCase.WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalCase.WebAPI.Interfaces
{
    public interface IPersonService<TId, TEntity> where TEntity : DomainObject
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TId id, TEntity entity);
        Task<bool> DeleteAsync(TId entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TId id);
    }
}
