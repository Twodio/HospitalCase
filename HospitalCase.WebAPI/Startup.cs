using HospitalCase.Application.Interfaces;
using HospitalCase.Application.Models;
using HospitalCase.Application.Services;
using HospitalCase.Domain.Models;
using HospitalCase.Insfrastructure;
using HospitalCase.Insfrastructure.Repositories;
using HospitalCase.Insfrastructure.Seeders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HospitalCase.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure and Register DbContext
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<HospitalCaseDbContext>(options => options.UseSqlServer(connectionString));

            // Register Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<HospitalCaseDbContext>()
                .AddDefaultTokenProviders();

            // Configure and register JWT Authentication
            // TODO: Use a class instead of key values
            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:key"]);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Register repositories
            services.AddScoped<IHealthcareProviderRepository>(s => new HealthcareProviderRepository(s.GetRequiredService<HospitalCaseDbContext>()));
            services.AddScoped<IPatientRepository>(s => new PatientRepository(s.GetRequiredService<HospitalCaseDbContext>()));
            services.AddScoped<IMedicalRecordRepository>(s => new MedicalRecordRepository(s.GetRequiredService<HospitalCaseDbContext>()));

            // Register application services
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IHealthcareProviderService>(s => new HealthcareProviderService(s.GetRequiredService<IHealthcareProviderRepository>()));
            services.AddScoped<IPatientService>(s => new PatientService(s.GetRequiredService<IPatientRepository>()));
            services.AddScoped<IMedicalRecordService>(s => new MedicalRecordService(s.GetRequiredService<IMedicalRecordRepository>()));

            // Register other dependencies
            services.AddTransient<IDesignTimeDbContextFactory<HospitalCaseDbContext>, HospitalCaseDbContextFactory>(s => s.GetRequiredService<HospitalCaseDbContextFactory>());

            // Register Controllers
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager)
        {
            // Middlewates
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Seed data
            RoleSeeder.SeedRolesAsync(roleManager).Wait();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
