using HospitalCase.WebAPI.Validators;

namespace HospitalCase.WebAPI.Interfaces
{
    public interface IValidator<T> where T : class
    {
        /// <summary>
        /// Validates the entity
        /// </summary>
        /// <param name="entity">The entity to be validated.</param>
        /// <returns>A validation result object containing the validation state and errors if any.</returns>
        ValidationResult Validate(T entity);
    }
}
