using HospitalCase.Application.Interfaces;
using HospitalCase.Domain.Interfaces;
using HospitalCase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalCase.Application.Services
{
    public class PersonService<TEntity> : IPersonService<int, TEntity> where TEntity : Person, new()
    {
        protected readonly IPersonRepository<TEntity> _personRepository;
        protected readonly IValidator<TEntity> _validator;

        public PersonService(IPersonRepository<TEntity> personRepository, IValidator<TEntity> validator)
        {
            _personRepository = personRepository;
            _validator = validator;
        }

        public async virtual Task<TEntity> CreateAsync(TEntity entity)
        {
            // Validate

            _validator.Validate(entity);

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

            _validator.Validate(entity);

            var result = await _personRepository.UpdateAsync(id, entity);

            // Check operation before returning

            return result;
        }
    }
}
