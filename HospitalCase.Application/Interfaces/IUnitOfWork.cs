using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HospitalCase.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task BeginTransationAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
