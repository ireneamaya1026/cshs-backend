// Application/Auth/DTOs/RefreshTokenRequest.cs
namespace CSHSBackendAPI.Application.Auth.DTOs;

public class RefreshTokenRequest
{
    public string Token { get; set; } = string.Empty;
}