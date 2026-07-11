using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Cloud.NaturalLanguage;

public class TextDocument
{
    public string Type { get; set; } = "PLAIN_TEXT"; // PLAIN_TEXT/HTML
    public string? Content { get; set; }
    public string? GcsContentUri { get; set; }
    public string? Language { get; set; }
}

public enum EncodingType
{
    NONE,
    UTF8,
    UTF16,
    UTF32
}

public class AnalyzeEntitiesRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public TextDocument Document { get; set; } = new();

    public string EncodingType { get; set; } = "UTF8";
}

public class TextSpan
{
    public string? Content { get; set; }
    public int BeginOffset { get; set; }
}

public class EntityMention
{
    public TextSpan? Text { get; set; }
    public string? Type { get; set; } // PROPER/COMMON
    public double Probability { get; set; }
}

public class Entity
{
    public string? Name { get; set; }
    public string? Type { get; set; } // UNKNOWN/PERSON/LOCATION/ORGANIZATION/EVENT/WORK_OF_ART/CONSUMER_GOOD/OTHER/PHONE_NUMBER/ADDRESS/DATE/NUMBER/PRICE
    public Dictionary<string, string>? Metadata { get; set; }
    public double Salience { get; set; }
    public List<EntityMention>? Mentions { get; set; }
    public Sentiment? Sentiment { get; set; }
}

public class AnalyzeEntitiesResponse
{
    public List<Entity>? Entities { get; set; }
    public string? Language { get; set; }
}

public class AnalyzeSentimentRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public TextDocument Document { get; set; } = new();

    public string EncodingType { get; set; } = "UTF8";
}

public class Sentiment
{
    public double Magnitude { get; set; }
    public double Score { get; set; }
}

public class TextSpanSentiment
{
    public TextSpan? Text { get; set; }
    public Sentiment? Sentiment { get; set; }
}

public class SentenceSentiment
{
    public TextSpan? Text { get; set; }
    public Sentiment? Sentiment { get; set; }
}

public class AnalyzeSentimentResponse
{
    public Sentiment? DocumentSentiment { get; set; }
    public string? Language { get; set; }
    public List<SentenceSentiment>? Sentences { get; set; }
}

public class AnalyzeSyntaxRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public TextDocument Document { get; set; } = new();

    public string EncodingType { get; set; } = "UTF8";
}

public class PartOfSpeech
{
    public string? Tag { get; set; }
    public string? Aspect { get; set; }
    public string? Case { get; set; }
    public string? Form { get; set; }
    public string? Gender { get; set; }
    public string? Mood { get; set; }
    public string? Number { get; set; }
    public string? Person { get; set; }
    public string? Proper { get; set; }
    public string? Reciprocity { get; set; }
    public string? Tense { get; set; }
    public string? Voice { get; set; }
}

public class DependencyEdge
{
    public int HeadTokenIndex { get; set; }
    public string? Label { get; set; }
}

public class Token
{
    public TextSpan? Text { get; set; }
    public PartOfSpeech? PartOfSpeech { get; set; }
    public DependencyEdge? DependencyEdge { get; set; }
    public string? Lemma { get; set; }
}

public class AnalyzeSyntaxResponse
{
    public List<Token>? Tokens { get; set; }
    public string? Language { get; set; }
}

public class ClassificationModelOptions
{
    public string? ModelType { get; set; }
}

public class ClassifyTextRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public TextDocument Document { get; set; } = new();

    public ClassificationModelOptions? ClassificationModelOptions { get; set; }
}

public class ClassificationCategory
{
    public string? Name { get; set; }
    public double Confidence { get; set; }
}

public class ClassifyTextResponse
{
    public List<ClassificationCategory>? Categories { get; set; }
}

public class ModerateTextRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public TextDocument Document { get; set; } = new();

    public string ModelVersion { get; set; } = "MODEL_VERSION_UNSPECIFIED";
}

public class ModerationCategory
{
    public string? Name { get; set; }
    public double Confidence { get; set; }
}

public class ModerateTextResponse
{
    public List<ModerationCategory>? ModerationCategories { get; set; }
}

public class AnnotateTextFeatures
{
    public bool ExtractSyntax { get; set; }
    public bool ExtractEntities { get; set; }
    public bool ExtractDocumentSentiment { get; set; }
    public bool ClassifyText { get; set; }
    public bool ModerateText { get; set; }
    public bool ExtractEntitySentiment { get; set; }
}

public class AnnotateTextRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public TextDocument Document { get; set; } = new();

    public AnnotateTextFeatures Features { get; set; } = new();
    public string EncodingType { get; set; } = "UTF8";
}

public class AnnotateTextResponse
{
    public List<SentenceSentiment>? Sentences { get; set; }
    public List<Token>? Tokens { get; set; }
    public List<Entity>? Entities { get; set; }
    public Sentiment? DocumentSentiment { get; set; }
    public string? Language { get; set; }
    public List<ClassificationCategory>? Categories { get; set; }
    public List<ModerationCategory>? ModerationCategories { get; set; }
}
