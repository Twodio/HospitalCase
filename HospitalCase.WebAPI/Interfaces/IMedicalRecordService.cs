using HospitalCase.WebAPI.Models;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HospitalCase.WebAPI.Interfaces
{
    /// <summary>
    /// Service responsible for managing medical records, ensuring integrity and enforcing business rules
    /// </summary>
    public interface IMedicalRecordService
    {
        /// <summary>
        /// Creates a new medical record
        /// </summary>
        /// <param name="entity">The medical record to create</param>
        /// <returns>The created medical record</returns>
        Task<MedicalRecord> CreateAsync(MedicalRecord entity);

        /// <summary>
        /// Updates an existing medical record
        /// </summary>
        /// <param name="id">The ID of the medical record to update</param>
        /// <param name="entity">The medical record details to update</param>
        /// <returns>The updated medical record entity</returns>
        Task<MedicalRecord> UpdateAsync(int id, MedicalRecord entity);

        /// <summary>
        /// Deletes a medical record by ID
        /// </summary>
        /// <param name="id">The ID of the medical record to delete</param>
        /// <returns>True if the operation is completed without error</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieves all the medical records
        /// </summary>
        /// <returns>A list of medical records</returns>
        Task<IEnumerable<MedicalRecord>> GetAllAsync();

        /// <summary>
        /// Retrieves a medical record by ID
        /// </summary>
        /// <param name="id">The ID of the medical record</param>
        /// <returns>The medical record details</returns>
        Task<MedicalRecord> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves all medical records by Healthcare Provider ID
        /// </summary>
        /// <param name="healthcareProviderId">The Healthcare Provider ID</param>
        /// <returns>A list of medical records</returns>
        Task<IEnumerable<MedicalRecord>> GetByHealthcareProviderId(int healthcareProviderId);

        /// <summary>
        /// Retrieves all medical records by Patient ID
        /// </summary>
        /// <param name="patientId">The Patient ID</param>
        /// <returns>A list of medical records</returns>
        Task<IEnumerable<MedicalRecord>> GetByPatientId(int patientId);
    }
}
