using HospitalCase.WebAPI.Models;

namespace HospitalCase.WebAPI.Interfaces
{
    public interface IPersonRepository<T> : IBaseRepository<int, T> where T : Person
    {
    }
}
