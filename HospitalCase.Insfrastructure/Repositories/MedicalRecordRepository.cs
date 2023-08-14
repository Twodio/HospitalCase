using HospitalCase.Application.Interfaces;
using HospitalCase.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalCase.Insfrastructure.Repositories
{
    public class MedicalRecordRepository : BaseRepository<MedicalRecord>, IMedicalRecordRepository
    {

        public MedicalRecordRepository(HospitalCaseDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
