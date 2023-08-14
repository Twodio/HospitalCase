using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalCase.WebAPI.Repositories
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly ICollection<MedicalRecord> _records;

        public MedicalRecordRepository(ICollection<MedicalRecord> records)
        {
            _records = records;
        }

        public Task<MedicalRecord> CreateAsync(MedicalRecord entity)
        {
            _records.Add(entity);

            return Task.FromResult(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var foundEntry = await GetByIdAsync(id);

            if (foundEntry == null)
            {
                return false;
            }

            _records.Remove(foundEntry);

            return true;
        }

        public Task<IEnumerable<MedicalRecord>> GetAllAsync()
        {
            var entries = _records.AsEnumerable();

            return Task.FromResult(entries);
        }

        public Task<MedicalRecord> GetByIdAsync(int id)
        {
            var foundEntry = _records.SingleOrDefault(e => e.Id == id);

            return Task.FromResult(foundEntry);
        }

        public async Task<MedicalRecord> UpdateAsync(int id, MedicalRecord entity)
        {
            var foundEntry = await GetByIdAsync(id);

            //if (foundEntry == null)
            //{
            //    return false;
            //}

            // Collections are reference types, updating the reference affects the collection instance
            foundEntry.Patient = entity.Patient;
            foundEntry.HealthcareProvider = entity.HealthcareProvider;
            foundEntry.RecordDate = entity.RecordDate;
            foundEntry.Symptoms = entity.Symptoms;
            foundEntry.Diagnosis = entity.Diagnosis;
            foundEntry.Treatment = entity.Treatment;
            foundEntry.Notes = entity.Notes;

            return foundEntry;
        }
    }
}
