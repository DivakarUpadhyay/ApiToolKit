using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Gmail;

public class LabelColor
{
    public string? TextColor { get; set; }
    public string? BackgroundColor { get; set; }
}

public class LabelResponse
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public int MessagesTotal { get; set; }
    public int MessagesUnread { get; set; }
    public int ThreadsTotal { get; set; }
    public int ThreadsUnread { get; set; }
    public string? MessageListVisibility { get; set; }
    public string? LabelListVisibility { get; set; }
    public LabelColor? Color { get; set; }
}

public class ListLabelsResponse
{
    public List<LabelResponse>? Labels { get; set; }
}

public class CreateLabelRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string MessageListVisibility { get; set; } = "show";
    public string LabelListVisibility { get; set; } = "labelShow";
    public string? TextColor { get; set; }
    public string? BackgroundColor { get; set; }
}

public class UpdateLabelRequest
{
    [Required]
    public string LabelId { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? MessageListVisibility { get; set; }
    public string? LabelListVisibility { get; set; }
    public string? TextColor { get; set; }
    public string? BackgroundColor { get; set; }
}

public class DeleteLabelRequest
{
    [Required]
    public string LabelId { get; set; } = string.Empty;
}
