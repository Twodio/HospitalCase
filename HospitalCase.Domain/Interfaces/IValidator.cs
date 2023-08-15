using HospitalCase.Domain.Validators;

namespace HospitalCase.Domain.Interfaces
{
    public interface IValidator<T> where T : class, new()
    {
        /// <summary>
        /// Validates the entity
        /// </summary>
        /// <param name="entity">The entity to be validated.</param>
        void Validate(T entity);
    }
}
