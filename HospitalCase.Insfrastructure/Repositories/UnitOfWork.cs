using HospitalCase.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace HospitalCase.Insfrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly HospitalCaseDbContext _hospitalCaseDbContext;
        private IDbContextTransaction _dbTransaction;

        public UnitOfWork(HospitalCaseDbContext hospitalCaseDbContext)
        {
            _hospitalCaseDbContext = hospitalCaseDbContext;
        }

        public async Task BeginTransationAsync()
        {
            _dbTransaction = await _hospitalCaseDbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _dbTransaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _dbTransaction.RollbackAsync();
        }

        public void Dispose()
        {
            _dbTransaction?.Dispose();
        }
    }
}
