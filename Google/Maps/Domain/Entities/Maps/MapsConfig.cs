namespace ApiToolKit.Domain.Entities.Maps;

public class MapsConfig
{
    public int Id { get; set; }
    public string ApiKey { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
