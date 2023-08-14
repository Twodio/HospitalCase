using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;
using HospitalCase.WebAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalCase.WebAPI.Services
{
    public class PersonService<TEntity> : IPersonService<int, TEntity> where TEntity : Person, new()
    {
        private readonly IPersonRepository<TEntity> _personRepository;

        public PersonService(IPersonRepository<TEntity> personRepository)
        {
            _personRepository = personRepository;
        }

        public async virtual Task<TEntity> CreateAsync(TEntity entity)
        {
            // Validate

            var result = await _personRepository.CreateAsync(entity);

            // Check operation before returning

            return result;
        }

        public async virtual Task<bool> DeleteAsync(int id)
        {
            // Validate

            var result = await _personRepository.DeleteAsync(id);

            // Check operation before returning

            return result;
        }

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var result = await _personRepository.GetAllAsync();

            return result;
        }

        public async virtual Task<TEntity> GetByIdAsync(int id)
        {
            // Validate

            var result = await _personRepository.GetByIdAsync(id);

            // Check operation before returning

            return result;
        }

        public async virtual Task<TEntity> UpdateAsync(int id, TEntity entity)
        {
            // Validate

            var result = await _personRepository.UpdateAsync(id, entity);

            // Check operation before returning

            return result;
        }
    }
}
