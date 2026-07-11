namespace ApiToolKit.Domain.Entities.Firebase;

public class FirebaseApiLog
{
    public int Id { get; set; }
    public string? ProjectId { get; set; }
    public string Product { get; set; } = string.Empty; // FCM, Auth, RemoteConfig, AppCheck
    public string ApiName { get; set; } = string.Empty;
    public string? RequestBody { get; set; }
    public string? ResponseBody { get; set; }
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public DateTime CreatedAt { get; set; }
}
