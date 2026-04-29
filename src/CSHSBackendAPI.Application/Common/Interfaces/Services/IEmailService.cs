namespace CSHSBackendAPI.Application.Common.Interfaces.Services;

public interface IEmailService
{
    Task SendEnrollmentConfirmationAsync(string toEmail, string studentName);
    Task SendEnrollmentApprovalAsync(string toEmail, string studentName);
    Task SendEnrollmentRejectionAsync(string toEmail, string studentName, string reason);
    Task SendPasswordResetAsync(string toEmail, string resetLink);
}