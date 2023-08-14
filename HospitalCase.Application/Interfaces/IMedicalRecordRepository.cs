using HospitalCase.Domain.Models;

namespace HospitalCase.Application.Interfaces
{
    public interface IMedicalRecordRepository : IBaseRepository<int, MedicalRecord>
    {
    }
}
