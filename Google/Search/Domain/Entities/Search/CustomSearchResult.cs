namespace ApiToolKit.Domain.Entities.Search;

public class CustomSearchResult
{
    public int Id { get; set; }
    public string Query { get; set; } = string.Empty;
    public string SearchEngineId { get; set; } = string.Empty;
    public long TotalResults { get; set; }
    public string? SearchType { get; set; }
    public string? ResultsJson { get; set; }
    public DateTime CreatedAt { get; set; }
}
