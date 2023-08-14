using HospitalCase.WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalCase.WebAPI.Interfaces
{
    public interface IBaseRepository<TId, TEntity> where TEntity : DomainObject, new()
    {
        /// <summary>
        /// Get all the values from the datasource
        /// </summary>
        /// <returns>All entries available from the datasource</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Get the value matching the id from the datasource
        /// </summary>
        /// <param name="id">The field to be matched against in the datasource</param>
        /// <returns>The found entry</returns>
        Task<TEntity> GetByIdAsync(TId id);

        // TODO: Not handling id conflict issues
        /// <summary>
        /// Creates a new entry on the datasource
        /// </summary>
        /// <param name="entity">The value to be added to the datasource</param>
        /// <returns>True if the operation has been successfully completed</returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Updates a value matching the specified id in the datasource
        /// </summary>
        /// <param name="id">The field to be matched against</param>
        /// <param name="entity">The value to be updated in the matching result</param>
        /// <returns>True if the operation has been successfully completed</returns>
        Task<TEntity> UpdateAsync(TId id, TEntity entity);

        /// <summary>
        /// Removes the matching entry id from the datasource
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the operations has been successfully completed</returns>
        Task<bool> DeleteAsync(TId id);
    }
}
