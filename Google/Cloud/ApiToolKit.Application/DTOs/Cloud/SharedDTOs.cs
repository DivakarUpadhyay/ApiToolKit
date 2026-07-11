namespace ApiToolKit.Application.DTOs.Cloud.Shared;

public class OperationError
{
    public int Code { get; set; }
    public string? Message { get; set; }
}

public class OperationResponse
{
    public string? Name { get; set; }
    public bool Done { get; set; }
    public OperationError? Error { get; set; }
    public object? Metadata { get; set; }
    public object? Response { get; set; }
}
