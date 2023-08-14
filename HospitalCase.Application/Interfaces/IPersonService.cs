using HospitalCase.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalCase.Application.Interfaces
{
    /// <summary>
    /// Service responsible for managing people, ensuring integrity and enforcing business rules
    /// </summary>
    /// <typeparam name="TId">The ID type</typeparam>
    /// <typeparam name="TEntity">The Entity type</typeparam>
    public interface IPersonService<TId, TEntity> where TEntity : DomainObject
    {
        /// <summary>
        /// Creates a new person
        /// </summary>
        /// <param name="entity">The person details to create</param>
        /// <returns>The created person details</returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Updates an existing person
        /// </summary>
        /// <param name="id">The ID of the person to update</param>
        /// <param name="entity">The person details to update</param>
        /// <returns>The updated person entity</returns>
        Task<TEntity> UpdateAsync(TId id, TEntity entity);

        /// <summary>
        /// Deletes a person by ID
        /// </summary>
        /// <param name="id">The ID of the person to delete</param>
        /// <returns>True if the operation is completed without error</returns>
        Task<bool> DeleteAsync(TId id);

        /// <summary>
        /// Retrieves all the people
        /// </summary>
        /// <returns>A list of people</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Retrieves a person by ID
        /// </summary>
        /// <param name="id">The ID of the person</param>
        /// <returns>The person details</returns>
        Task<TEntity> GetByIdAsync(TId id);
    }
}
