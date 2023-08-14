using HospitalCase.WebAPI.Models;

namespace HospitalCase.WebAPI.Interfaces
{
    public interface IPersonRepository<TEntity> : IBaseRepository<int, TEntity> where TEntity : Person, new()
    {
    }
}
