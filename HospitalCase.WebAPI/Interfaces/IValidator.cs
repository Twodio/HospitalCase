using HospitalCase.WebAPI.Validators;

namespace HospitalCase.WebAPI.Interfaces
{
    public interface IValidator<T> where T : class
    {
        ValidationResult Validate(T entity);
    }
}
