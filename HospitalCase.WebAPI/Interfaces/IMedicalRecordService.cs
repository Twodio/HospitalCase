using HospitalCase.WebAPI.Models;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HospitalCase.WebAPI.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<MedicalRecord> CreateAsync(MedicalRecord entity);
        Task<MedicalRecord> UpdateAsync(int id, MedicalRecord entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<MedicalRecord>> GetAllAsync();
        Task<MedicalRecord> GetByIdAsync(int id);
        Task<IEnumerable<MedicalRecord>> GetByHealthcareProviderId(int healthcareProviderId);
        Task<IEnumerable<MedicalRecord>> GetByPatientId(int patientId);
    }
}
