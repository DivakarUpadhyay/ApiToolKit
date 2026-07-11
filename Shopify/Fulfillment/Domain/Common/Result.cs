namespace ApiToolKit.Domain.Common;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }
    public string? ErrorMessage { get; private set; }
    public int? StatusCode { get; private set; }

    private Result() { }

    public static Result<T> Ok(T data) => new() { IsSuccess = true, Data = data };

    public static Result<T> Fail(string errorMessage, int? statusCode = null) =>
        new() { IsSuccess = false, ErrorMessage = errorMessage, StatusCode = statusCode };
}

public class Result
{
    public bool IsSuccess { get; private set; }
    public string? ErrorMessage { get; private set; }
    public int? StatusCode { get; private set; }

    private Result() { }

    public static Result Ok() => new() { IsSuccess = true };

    public static Result Fail(string errorMessage, int? statusCode = null) =>
        new() { IsSuccess = false, ErrorMessage = errorMessage, StatusCode = statusCode };
}
