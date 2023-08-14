using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalCase.WebAPI.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public async Task<MedicalRecord> CreateAsync(MedicalRecord entity)
        {
            // Validate

            var result = await _medicalRecordRepository.CreateAsync(entity);

            // Check operation before returning

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Validate

            var result = await _medicalRecordRepository.DeleteAsync(id);

            // Check operation before returning

            return result;
        }

        public async Task<IEnumerable<MedicalRecord>> GetAllAsync()
        {
            var result = await _medicalRecordRepository.GetAllAsync();

            return result;
        }

        public async Task<MedicalRecord> GetByIdAsync(int id)
        {
            // Validate

            var result = await _medicalRecordRepository.GetByIdAsync(id);

            // Check operation before returning

            return result;
        }

        public async Task<MedicalRecord> UpdateAsync(int id, MedicalRecord entity)
        {
            // Validate

            var result = await _medicalRecordRepository.UpdateAsync(id, entity);

            // Check operation before returning

            return result;
        }

        public Task<IEnumerable<MedicalRecord>> GetByPatientId(int patientId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MedicalRecord>> GetByHealthcareProviderId(int healthcareProviderId)
        {
            throw new System.NotImplementedException();
        }
    }
}
