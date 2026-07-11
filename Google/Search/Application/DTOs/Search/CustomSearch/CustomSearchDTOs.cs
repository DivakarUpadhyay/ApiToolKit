using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Search.CustomSearch;

public class CustomSearchRequest
{
    [Required]
    public string Q { get; set; } = string.Empty;

    public string? Cx { get; set; }
    public int Num { get; set; } = 10;
    public int Start { get; set; } = 1;
    public string Language { get; set; } = "en";
    public string? CountryRestrict { get; set; }
    public string? DateRestrict { get; set; }
    public string? ExactTerms { get; set; }
    public string? ExcludeTerms { get; set; }
    public string? FileType { get; set; }
    public string Filter { get; set; } = "1";
    public string? Gl { get; set; }
    public string? Hq { get; set; }
    public string? LinkSite { get; set; }
    public string? LowRange { get; set; }
    public string? HighRange { get; set; }
    public string? OrTerms { get; set; }
    public string? Rights { get; set; }
    public string Safe { get; set; } = "off";
    public string? SearchType { get; set; }
    public string? SiteSearch { get; set; }
    public string? SiteSearchFilter { get; set; }
    public string? Sort { get; set; }
}

public class ImageSearchRequest
{
    [Required]
    public string Q { get; set; } = string.Empty;

    public string? Cx { get; set; }
    public int Num { get; set; } = 10;
    public int Start { get; set; } = 1;
    public string? Gl { get; set; }
    public string? Language { get; set; }
    public string? ImageColorType { get; set; }
    public string? ImageDominantColor { get; set; }
    public string? ImageSize { get; set; }
    public string? ImageType { get; set; }
    public string? FileType { get; set; }
    public string? Rights { get; set; }
    public string Safe { get; set; } = "off";
}

public class CustomSearchImage
{
    public string? ContextLink { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public int ByteSize { get; set; }
    public string? ThumbnailLink { get; set; }
    public int ThumbnailHeight { get; set; }
    public int ThumbnailWidth { get; set; }
}

public class CustomSearchResultItem
{
    public string? Kind { get; set; }
    public string? Title { get; set; }
    public string? HtmlTitle { get; set; }
    public string? Link { get; set; }
    public string? DisplayLink { get; set; }
    public string? Snippet { get; set; }
    public string? HtmlSnippet { get; set; }
    public string? CacheId { get; set; }
    public string? FormattedUrl { get; set; }
    public string? HtmlFormattedUrl { get; set; }
    public Dictionary<string, object>? PageMap { get; set; }
    public CustomSearchImage? Image { get; set; }
}

public class CustomSearchSpelling
{
    public string? CorrectedQuery { get; set; }
    public string? HtmlCorrectedQuery { get; set; }
}

public class CustomSearchQueryInfo
{
    public string? Title { get; set; }
    public string? TotalResults { get; set; }
    public string? SearchTerms { get; set; }
    public int Count { get; set; }
    public int StartIndex { get; set; }
    public string? Language { get; set; }
    public string? InputEncoding { get; set; }
    public string? OutputEncoding { get; set; }
    public string? Safe { get; set; }
    public string? Cx { get; set; }
}

public class CustomSearchQueriesBlock
{
    public List<CustomSearchQueryInfo>? Request { get; set; }
    public List<CustomSearchQueryInfo>? NextPage { get; set; }
    public List<CustomSearchQueryInfo>? PreviousPage { get; set; }
}

public class CustomSearchInformation
{
    public double SearchTime { get; set; }
    public string? FormattedSearchTime { get; set; }
    public string? TotalResults { get; set; }
    public string? FormattedTotalResults { get; set; }
}

public class CustomSearchResponse
{
    public string? Kind { get; set; }
    public object? Url { get; set; }
    public CustomSearchQueriesBlock? Queries { get; set; }
    public CustomSearchSpelling? Spelling { get; set; }
    public CustomSearchInformation? SearchInformation { get; set; }
    public List<CustomSearchResultItem>? Items { get; set; }
    public object? Context { get; set; }
}
