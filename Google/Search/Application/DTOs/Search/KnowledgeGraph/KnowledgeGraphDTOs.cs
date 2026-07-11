using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Search.KnowledgeGraph;

public class KnowledgeGraphRequest
{
    [Required]
    public string Query { get; set; } = string.Empty;

    public List<string>? Types { get; set; }
    public string LanguageCode { get; set; } = "en";
    public int Limit { get; set; } = 10;
    public bool Indent { get; set; } = false;
    public bool Prefix { get; set; } = false;
}

public class KnowledgeGraphEntityType
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}

public class KnowledgeGraphImage
{
    public string? ContentUrl { get; set; }
    public string? Url { get; set; }
}

public class KnowledgeGraphDetailedDescription
{
    public string? ArticleBody { get; set; }
    public string? Url { get; set; }
    public string? License { get; set; }
}

public class KnowledgeGraphResultItem
{
    public string? Id { get; set; }
    public KnowledgeGraphEntityType? Type { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public KnowledgeGraphImage? Image { get; set; }
    public KnowledgeGraphDetailedDescription? DetailedDescription { get; set; }
    public string? Url { get; set; }
    public double ResultScore { get; set; }
}

public class KnowledgeGraphListElement
{
    public string? Type { get; set; }
    public KnowledgeGraphResultItem? Result { get; set; }
    public double ResultScore { get; set; }
}

public class KnowledgeGraphResponse
{
    public object? Context { get; set; }
    public string? Type { get; set; }
    public List<KnowledgeGraphListElement>? ItemListElement { get; set; }
}
