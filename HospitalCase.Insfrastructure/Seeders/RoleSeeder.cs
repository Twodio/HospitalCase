using HospitalCase.Application.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HospitalCase.Insfrastructure.Seeders
{
    /// <summary>
    /// Seed user roles
    /// </summary>
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(UserRoles.Patient))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Patient));
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.Doctor))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Doctor));
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.Nurse))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Nurse));
            }
        }
    }
}
