namespace CSHSBackendAPI.Domain.Enums;

public enum DocumentRequestStatus
{
    Requested,
    Processing,
    ForPayment,
    Ready,
    Released,
    Cancelled
}