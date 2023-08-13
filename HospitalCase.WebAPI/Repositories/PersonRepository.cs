using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalCase.WebAPI.Repositories
{
    public class PersonRepository<T> : IPersonRepository<T> where T : Person
    {
        protected readonly ICollection<T> _people;

        public PersonRepository(ICollection<T> people)
        {
            _people = people;
        }

        public Task<bool> CreateAsync(T entity)
        {
            _people.Add(entity);

            return Task.FromResult(true);
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

        public Task<IEnumerable<T>> GetAllAsync()
        {
            var entries = _people.AsEnumerable();

            return Task.FromResult(entries);
        }

        public Task<T> GetByIdAsync(int id)
        {
            var foundEntry = _people.SingleOrDefault(e => e.Id == id);

            return Task.FromResult(foundEntry);
        }

        public async Task<bool> UpdateAsync(int id, T entity)
        {
            var foundEntry = await GetByIdAsync(id);

            if (foundEntry == null)
            {
                return false;
            }

            // Collections are reference types, updating the reference affects the collection instance
            foundEntry.FirstName = entity.FirstName;
            foundEntry.LastName = entity.LastName;
            foundEntry.BirthDate = entity.BirthDate;
            foundEntry.PhoneNumber = entity.PhoneNumber;

            return true;
        }
    }
}
