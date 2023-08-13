using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalCase.WebAPI.Interfaces
{
    public interface IBaseRepository<TId, TValue> where TValue : class
    {
        /// <summary>
        /// Get all the values from the datasource
        /// </summary>
        /// <returns>All entries available from the datasource</returns>
        Task<IEnumerable<TValue>> GetAllAsync();

        /// <summary>
        /// Get the value matching the id from the datasource
        /// </summary>
        /// <param name="id">The field to be matched against in the datasource</param>
        /// <returns>The found entry</returns>
        Task<TValue> GetByIdAsync(TId id);

        // TODO: Not handling id conflict issues
        /// <summary>
        /// Creates a new entry on the datasource
        /// </summary>
        /// <param name="entity">The value to be added to the datasource</param>
        /// <returns>True if the operation has been successfully completed</returns>
        Task<bool> CreateAsync(TValue entity);

        /// <summary>
        /// Updates a value matching the specified id in the datasource
        /// </summary>
        /// <param name="id">The field to be matched against</param>
        /// <param name="entity">The value to be updated in the matching result</param>
        /// <returns>True if the operation has been successfully completed</returns>
        Task<bool> UpdateAsync(TId id, TValue entity);

        /// <summary>
        /// Removes the matching entry id from the datasource
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the operations has been successfully completed</returns>
        Task<bool> DeleteAsync(TId id);
    }
}
