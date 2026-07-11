namespace ApiToolKit.Domain.Entities.Facebook;

public class FacebookCatalog
{
    public int Id { get; set; }
    public string CatalogId { get; set; } = string.Empty;
    public string BusinessId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? VerticalType { get; set; }
    public int ProductCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
