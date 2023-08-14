using HospitalCase.Domain.Models;

namespace HospitalCase.Application.Interfaces
{
    public interface IPersonRepository<TEntity> : IBaseRepository<int, TEntity> where TEntity : Person, new()
    {
    }
}
