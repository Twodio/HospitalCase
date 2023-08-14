using Microsoft.EntityFrameworkCore;
using System;

namespace HospitalCase.Insfrastructure
{
    public class HospitalCaseDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _optionBuilder;

        public HospitalCaseDbContextFactory(Action<DbContextOptionsBuilder> optionBuilder)
        {
            _optionBuilder = optionBuilder;
        }

        public HospitalCaseDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<HospitalCaseDbContext> options = new DbContextOptionsBuilder<HospitalCaseDbContext>();

            _optionBuilder(options);

            return new HospitalCaseDbContext(options.Options);
        }
    }
}
