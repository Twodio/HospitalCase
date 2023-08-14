using HospitalCase.Application.Interfaces;
using HospitalCase.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalCase.Insfrastructure.Repositories
{
    public class PersonRepository<TEntity> : BaseRepository<TEntity>, IPersonRepository<TEntity> where TEntity : Person, new()
    {
        public PersonRepository(HospitalCaseDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
