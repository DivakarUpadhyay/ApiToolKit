using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.GoogleAds.Audience;

public class ListAudiencesRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    public int PageSize { get; set; } = 50;
}

public class AudienceResponse
{
    public string ResourceName { get; set; } = string.Empty;
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

public class CreateAudienceRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
