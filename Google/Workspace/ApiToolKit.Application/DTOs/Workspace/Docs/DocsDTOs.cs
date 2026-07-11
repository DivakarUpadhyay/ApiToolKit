using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Docs;

public class TextStyle
{
    public bool Bold { get; set; }
    public bool Italic { get; set; }
    public bool Underline { get; set; }
    public bool Strikethrough { get; set; }
    public object? FontSize { get; set; }
    public object? WeightedFontFamily { get; set; }
    public object? ForegroundColor { get; set; }
    public object? BackgroundColor { get; set; }
}

public class TextRun
{
    public string? Content { get; set; }
    public TextStyle? TextStyle { get; set; }
}

public class ParagraphTextElement
{
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
    public TextRun? TextRun { get; set; }
    public object? InlineObjectElement { get; set; }
}

public class ParagraphElement
{
    public List<ParagraphTextElement>? Elements { get; set; }
    public object? ParagraphStyle { get; set; }
    public object? Bullet { get; set; }
}

public class TableCell
{
    public object? Content { get; set; }
    public object? TableCellStyle { get; set; }
}

public class TableRow
{
    public List<TableCell>? TableCells { get; set; }
    public object? TableRowStyle { get; set; }
}

public class Table
{
    public int Rows { get; set; }
    public int Columns { get; set; }
    public List<TableRow>? TableRows { get; set; }
    public object? TableStyle { get; set; }
}

public class StructuralElement
{
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
    public ParagraphElement? Paragraph { get; set; }
    public Table? Table { get; set; }
    public object? SectionBreak { get; set; }
    public object? TableOfContents { get; set; }
}

public class DocumentBody
{
    public List<StructuralElement>? Content { get; set; }
}

public class DocumentResponse
{
    public string? DocumentId { get; set; }
    public string? Title { get; set; }
    public string? RevisionId { get; set; }
    public DocumentBody? Body { get; set; }
    public object? DocumentStyle { get; set; }
    public object? NamedStyles { get; set; }
    public object? Headers { get; set; }
    public object? Footers { get; set; }
    public string? DocumentUrl { get; set; }
}

public class CreateDocumentRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;
}

public class GetDocumentRequest
{
    [Required]
    public string DocumentId { get; set; } = string.Empty;
}

public class InsertTextRequest
{
    [Required]
    public string DocumentId { get; set; } = string.Empty;

    [Required]
    public string Text { get; set; } = string.Empty;
    public int Index { get; set; } = 1;
}

public class ReplaceTextRequest
{
    [Required]
    public string DocumentId { get; set; } = string.Empty;

    [Required]
    public string OldText { get; set; } = string.Empty;

    [Required]
    public string NewText { get; set; } = string.Empty;
    public bool MatchCase { get; set; } = true;
}

public class InsertTableRequest
{
    [Required]
    public string DocumentId { get; set; } = string.Empty;

    [Required]
    public int Rows { get; set; }

    [Required]
    public int Columns { get; set; }
    public int Index { get; set; } = 1;
}

public class DeleteContentRangeRequest
{
    [Required]
    public string DocumentId { get; set; } = string.Empty;

    [Required]
    public int StartIndex { get; set; }

    [Required]
    public int EndIndex { get; set; }
}

public class InsertPageBreakRequest
{
    [Required]
    public string DocumentId { get; set; } = string.Empty;
    public int? Index { get; set; }
}

public class ParagraphStyleRequest
{
    [Required]
    public string DocumentId { get; set; } = string.Empty;

    [Required]
    public int StartIndex { get; set; }

    [Required]
    public int EndIndex { get; set; }

    [Required]
    public string NamedStyleType { get; set; } = string.Empty;
}
