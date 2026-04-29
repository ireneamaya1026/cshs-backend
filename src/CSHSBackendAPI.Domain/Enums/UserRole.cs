namespace CSHSBackendAPI.Domain.Enums;

public enum UserRole
{
    SuperAdmin,      // your company - manages all schools
    SchoolAdmin,     // manages one school
    SystemAdmin,     // manages one campus
    Registrar,       // handles enrollment approvals
    Accounting,        // handles billing and payments
    Teacher,
    Student
}