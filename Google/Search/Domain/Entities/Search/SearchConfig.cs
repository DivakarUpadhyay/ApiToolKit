namespace ApiToolKit.Domain.Entities.Search;

public class SearchConfig
{
    public int Id { get; set; }
    public string ApiKey { get; set; } = string.Empty;
    public string SearchEngineId { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
