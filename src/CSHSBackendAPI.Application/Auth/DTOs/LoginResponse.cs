namespace CSHSBackendAPI.Application.Auth.DTOs;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public UserResponse User { get; set; } = new();
}

public class UserResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public CampusResponse? Campus { get; set; }
}

public class CampusResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CampusKey { get; set; } = string.Empty;
}