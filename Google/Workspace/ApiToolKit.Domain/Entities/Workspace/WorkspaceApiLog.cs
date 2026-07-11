namespace ApiToolKit.Domain.Entities.Workspace;

public class WorkspaceApiLog
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Product { get; set; } = string.Empty;
    public string ApiName { get; set; } = string.Empty;
    public string? RequestBody { get; set; }
    public string? ResponseBody { get; set; }
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public DateTime CreatedAt { get; set; }
}
