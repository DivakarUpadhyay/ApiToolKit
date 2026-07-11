namespace ApiToolKit.Domain.Common;
public class Result<T>
{
    public bool Success { get; private set; }
    public T? Data { get; private set; }
    public string? Message { get; private set; }
    public int? ErrorCode { get; private set; }
    public static Result<T> Ok(T data, string? message = null) => new() { Success = true, Data = data, Message = message };
    public static Result<T> Fail(string message, int? errorCode = null) => new() { Success = false, Message = message, ErrorCode = errorCode };
}
public class Result
{
    public bool Success { get; private set; }
    public string? Message { get; private set; }
    public static Result Ok(string? message = null) => new() { Success = true, Message = message };
    public static Result Fail(string message) => new() { Success = false, Message = message };
}
