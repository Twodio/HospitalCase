using HospitalCase.Application.Interfaces;
using HospitalCase.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalCase.Insfrastructure.Repositories
{
    public class PersonRepository<TEntity> : IPersonRepository<TEntity> where TEntity : Person, new()
    {
        protected readonly ICollection<TEntity> _people;

        public PersonRepository(ICollection<TEntity> people)
        {
            _people = people;
        }

        public Task<TEntity> CreateAsync(TEntity entity)
        {
            _people.Add(entity);

            return Task.FromResult(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var foundEntry = await GetByIdAsync(id);

            if (foundEntry == null)
            {
                return false;
            }

            _people.Remove(foundEntry);

            return true;
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var entries = _people.AsEnumerable();

            return Task.FromResult(entries);
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            var foundEntry = _people.SingleOrDefault(e => e.Id == id);

            return Task.FromResult(foundEntry);
        }

        public async Task<TEntity> UpdateAsync(int id, TEntity entity)
        {
            var foundEntry = await GetByIdAsync(id);

            //if (foundEntry == null)
            //{
            //    return false;
            //}

            // Collections are reference types, updating the reference affects the collection instance
            foundEntry.FirstName = entity.FirstName;
            foundEntry.LastName = entity.LastName;
            foundEntry.BirthDate = entity.BirthDate;
            foundEntry.PhoneNumber = entity.PhoneNumber;

            return foundEntry;
        }
    }
}
