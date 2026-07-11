using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Slides;

public class Dimension
{
    public double Magnitude { get; set; }
    public string? Unit { get; set; }
}

public class Size
{
    public Dimension? Width { get; set; }
    public Dimension? Height { get; set; }
}

public class AffineTransform
{
    public double ScaleX { get; set; }
    public double ScaleY { get; set; }
    public double ShearX { get; set; }
    public double ShearY { get; set; }
    public double TranslateX { get; set; }
    public double TranslateY { get; set; }
    public string? Unit { get; set; }
}

public class TextStyle
{
    public bool Bold { get; set; }
    public bool Italic { get; set; }
    public bool Underline { get; set; }
    public object? FontSize { get; set; }
    public object? ForegroundColor { get; set; }
}

public class TextElementDto
{
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
    public object? TextRun { get; set; }
    public object? ParagraphMarker { get; set; }
}

public class TextContent
{
    public List<TextElementDto>? TextElements { get; set; }
}

public class ShapeDto
{
    public string? ShapeType { get; set; }
    public TextContent? Text { get; set; }
    public object? ShapeProperties { get; set; }
}

public class ImageDto
{
    public string? ContentUrl { get; set; }
    public string? SourceUrl { get; set; }
    public object? ImageProperties { get; set; }
}

public class PageElementDto
{
    public string? ObjectId { get; set; }
    public Size? Size { get; set; }
    public AffineTransform? Transform { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public ShapeDto? Shape { get; set; }
    public ImageDto? Image { get; set; }
    public object? Table { get; set; }
    public object? Line { get; set; }
    public object? SheetsChart { get; set; }
}

public class SlideProperties
{
    public string? LayoutObjectId { get; set; }
    public string? MasterObjectId { get; set; }
    public object? NotesPage { get; set; }
}

public class SlideDto
{
    public string? ObjectId { get; set; }
    public SlideProperties? SlideProperties { get; set; }
    public List<PageElementDto>? PageElements { get; set; }
    public string? PageType { get; set; }
    public object? SlideBackgroundFill { get; set; }
}

public class PresentationResponse
{
    public string? PresentationId { get; set; }
    public string? Title { get; set; }
    public List<SlideDto>? Slides { get; set; }
    public Size? PageSize { get; set; }
    public string? RevisionId { get; set; }
    public object? NotesMaster { get; set; }
    public string? Locale { get; set; }
    public string? PresentationUrl { get; set; }
}

public class CreatePresentationRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;
}

public class GetPresentationRequest
{
    [Required]
    public string PresentationId { get; set; } = string.Empty;
}

public class SlideLayoutReference
{
    public string PredefinedLayout { get; set; } = "BLANK";
}

public class AddSlideRequest
{
    [Required]
    public string PresentationId { get; set; } = string.Empty;
    public int? InsertionIndex { get; set; }
    public SlideLayoutReference? SlideLayoutReference { get; set; }
}

public class DeleteSlideRequest
{
    [Required]
    public string PresentationId { get; set; } = string.Empty;

    [Required]
    public string SlideObjectId { get; set; } = string.Empty;
}

public class SlidesInsertTextRequest
{
    [Required]
    public string PresentationId { get; set; } = string.Empty;

    [Required]
    public string ObjectId { get; set; } = string.Empty;

    [Required]
    public string Text { get; set; } = string.Empty;
    public int InsertionIndex { get; set; } = 0;
}

public class SlidesReplaceTextRequest
{
    [Required]
    public string PresentationId { get; set; } = string.Empty;

    [Required]
    public string OldText { get; set; } = string.Empty;

    [Required]
    public string NewText { get; set; } = string.Empty;
    public bool MatchCase { get; set; } = true;
    public List<string>? PageObjectIds { get; set; }
}

public class InsertImageRequest
{
    [Required]
    public string PresentationId { get; set; } = string.Empty;

    [Required]
    public string Url { get; set; } = string.Empty;

    [Required]
    public string SlideObjectId { get; set; } = string.Empty;
    public double Left { get; set; }
    public double Top { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
}

public class DuplicateSlideRequest
{
    [Required]
    public string PresentationId { get; set; } = string.Empty;

    [Required]
    public string SlideObjectId { get; set; } = string.Empty;
}

public class ReorderSlidesRequest
{
    [Required]
    public string PresentationId { get; set; } = string.Empty;

    [Required]
    public List<string> SlideObjectIds { get; set; } = new();

    [Required]
    public int InsertionIndex { get; set; }
}
