using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace HospitalCase.Insfrastructure
{
    public class HospitalCaseDbContextFactory : IDesignTimeDbContextFactory<HospitalCaseDbContext>
    {
        public HospitalCaseDbContext CreateDbContext(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            DbContextOptionsBuilder<HospitalCaseDbContext> optionsBuilder = new DbContextOptionsBuilder<HospitalCaseDbContext>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new HospitalCaseDbContext(optionsBuilder.Options);
        }
    }
}
