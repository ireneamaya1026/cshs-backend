// Domain/Enums/UserRole.cs
namespace CSHSBackendAPI.Domain.Enums;

public enum UserRole
{
    technical_admin,   // Super Admin — full access
    admin,             // Owner — GET only
    teacher,           // Grades, attendance, subject loads
    registrar_basic,   // Basic Ed enrollments
    registrar_college, // College enrollments
    accounting,        // Payments, fees, clearance
    principal_basic,   // Subject loads, grade oversight
    program_head       // College programs, grade oversight
}