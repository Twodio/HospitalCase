using Microsoft.AspNetCore.Authorization;

namespace HospitalCase.Application.Common
{
    /// <summary>
    /// Authorization polcies for medical records
    /// </summary>
    public static class MedicalRecordsPolicies
    {
        public const string View = "MedicalRecordsPolicies.View";
        public const string Create = "MedicalRecordsPolicies.Create";
        public const string Edit = "MedicalRecordsPolicies.Edit";

        public static void AddMedicalRecordsPolicies(this AuthorizationOptions options)
        {
            options.AddPolicy(View, policy =>
            {
                policy.RequireRole(UserRoles.Patient, UserRoles.Nurse, UserRoles.Doctor);
            });

            options.AddPolicy(Create, policy =>
            {
                policy.RequireRole(UserRoles.Doctor);
            });

            options.AddPolicy(Edit, policy =>
            {
                policy.RequireRole(UserRoles.Nurse, UserRoles.Doctor);
            });
        }
    }

    /// <summary>
    /// Authorization polcies for healthcare providers
    /// </summary>
    public static class HealthcareProvidersPolicies
    {
        public const string View = "HealthcareProvidersPolicies.View";
        public const string Create = "HealthcareProvidersPolicies.Create";
        public const string Edit = "HealthcareProvidersPolicies.Edit";

        public static void AddHealthcareProvidersPolicies(this AuthorizationOptions options)
        {
            options.AddPolicy(View, policy =>
            {
                policy.RequireRole(UserRoles.Patient, UserRoles.Nurse, UserRoles.Doctor);
            });

            options.AddPolicy(Create, policy =>
            {
                policy.RequireRole(UserRoles.Nurse, UserRoles.Doctor);
            });

            options.AddPolicy(Edit, policy =>
            {
                policy.RequireRole(UserRoles.Nurse, UserRoles.Doctor);
            });
        }
    }

    /// <summary>
    /// Authorization polcies for patients
    /// </summary>
    public static class PatientsPolicies
    {
        public const string View = "PatientsPolicies.View";
        public const string Create = "PatientsPolicies.Create";
        public const string Edit = "PatientsPolicies.Edit";

        public static void AddPatientsPolicies(this AuthorizationOptions options)
        {
            options.AddPolicy(View, policy =>
            {
                policy.RequireRole(UserRoles.Patient, UserRoles.Nurse, UserRoles.Doctor);
            });

            options.AddPolicy(Create, policy =>
            {
                policy.RequireRole(UserRoles.Patient);
            });

            options.AddPolicy(Edit, policy =>
            {
                policy.RequireRole(UserRoles.Patient);
            });
        }
    }
}
