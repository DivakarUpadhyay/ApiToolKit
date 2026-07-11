using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.Reaction;

public class CreateReactionRequest
{
    [Required] public string ObjectId { get; set; } = string.Empty;
    [Required] public string Type { get; set; } = "LIKE";
}

public class GetReactionsRequest
{
    [Required] public string ObjectId { get; set; } = string.Empty;
    public string? Type { get; set; }
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
}

public class ReactionResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Pic { get; set; }
}

public class ReactionSummary
{
    public int TotalCount { get; set; }
    public string? ViewerReaction { get; set; }
}

public class ReactionPaging
{
    public string? Before { get; set; }
    public string? After { get; set; }
}

public class GetReactionsResponse
{
    public List<ReactionResponse> Data { get; set; } = new();
    public ReactionPaging? Paging { get; set; }
    public ReactionSummary? Summary { get; set; }
}
