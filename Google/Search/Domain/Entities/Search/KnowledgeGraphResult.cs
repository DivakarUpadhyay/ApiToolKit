namespace ApiToolKit.Domain.Entities.Search;

public class KnowledgeGraphResult
{
    public int Id { get; set; }
    public string Query { get; set; } = string.Empty;
    public string? Types { get; set; }
    public string? ResultsJson { get; set; }
    public DateTime CreatedAt { get; set; }
}
