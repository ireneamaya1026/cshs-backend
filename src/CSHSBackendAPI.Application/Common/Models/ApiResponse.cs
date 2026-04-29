namespace CSHSBackendAPI.Application.Common.Models;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    // Success response with data
    public static ApiResponse<T> Ok(T data, string message = "Request successful.") =>
        new() { Success = true, Message = message, Data = data };

    // Success response without data
    public static ApiResponse<T> Ok(string message = "Request successful.") =>
        new() { Success = true, Message = message };

    // Failure response
    public static ApiResponse<T> Fail(string message, List<string>? errors = null) =>
        new() { Success = false, Message = message, Errors = errors };
}