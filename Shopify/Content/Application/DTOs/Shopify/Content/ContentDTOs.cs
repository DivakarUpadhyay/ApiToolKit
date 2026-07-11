using System.ComponentModel.DataAnnotations;
namespace ApiToolKit.Application.DTOs.Shopify.Content;

// ---- Page DTOs ----
public class PageDto { public string? Id { get; set; } public string? Title { get; set; } public string? BodyHtml { get; set; } public string? Handle { get; set; } public string? Author { get; set; } public string? TemplateSuffix { get; set; } public bool? Published { get; set; } public string? PublishedAt { get; set; } public string? CreatedAt { get; set; } public string? UpdatedAt { get; set; } }
public class ListPagesResponse { public List<PageDto>? Pages { get; set; } }
public class PageResponse { public PageDto? Page { get; set; } }
public class CreatePageRequest { [Required] public string Title { get; set; } = string.Empty; public string? BodyHtml { get; set; } public string? Author { get; set; } public bool? Published { get; set; } public string? TemplateSuffix { get; set; } }
public class UpdatePageRequest { public string? Title { get; set; } public string? BodyHtml { get; set; } public bool? Published { get; set; } public string? Author { get; set; } }

// ---- Blog DTOs ----
public class BlogDto { public string? Id { get; set; } public string? Title { get; set; } public string? Handle { get; set; } public string? Commentable { get; set; } public string? Feedburner { get; set; } public string? FeedburnerLocation { get; set; } public string? CreatedAt { get; set; } public string? UpdatedAt { get; set; } }
public class ListBlogsResponse { public List<BlogDto>? Blogs { get; set; } }
public class BlogResponse { public BlogDto? Blog { get; set; } }
public class CreateBlogRequest { [Required] public string Title { get; set; } = string.Empty; public string? Commentable { get; set; } public string? TemplateSuffix { get; set; } }
public class UpdateBlogRequest { public string? Title { get; set; } public string? Commentable { get; set; } }

// ---- Article DTOs ----
public class ArticleDto { public string? Id { get; set; } public string? BlogId { get; set; } public string? Title { get; set; } public string? BodyHtml { get; set; } public string? Author { get; set; } public string? Handle { get; set; } public string? Tags { get; set; } public bool? Published { get; set; } public string? PublishedAt { get; set; } public string? SummaryHtml { get; set; } public string? CreatedAt { get; set; } public string? UpdatedAt { get; set; } }
public class ListArticlesResponse { public List<ArticleDto>? Articles { get; set; } }
public class ArticleResponse { public ArticleDto? Article { get; set; } }
public class CreateArticleRequest { [Required] public string Title { get; set; } = string.Empty; public string? BodyHtml { get; set; } public string? Author { get; set; } public string? Tags { get; set; } public bool? Published { get; set; } public string? SummaryHtml { get; set; } }
public class UpdateArticleRequest { public string? Title { get; set; } public string? BodyHtml { get; set; } public string? Tags { get; set; } public bool? Published { get; set; } public string? Author { get; set; } }

// ---- Theme DTOs ----
public class ThemeDto { public string? Id { get; set; } public string? Name { get; set; } public string? Role { get; set; } public string? ThemeStoreId { get; set; } public bool? Previewable { get; set; } public bool? Processing { get; set; } public string? CreatedAt { get; set; } public string? UpdatedAt { get; set; } }
public class ListThemesResponse { public List<ThemeDto>? Themes { get; set; } }
public class ThemeResponse { public ThemeDto? Theme { get; set; } }
public class CreateThemeRequest { [Required] public string Name { get; set; } = string.Empty; [Required] public string Src { get; set; } = string.Empty; public string? Role { get; set; } }
public class UpdateThemeRequest { public string? Name { get; set; } public string? Role { get; set; } }

// ---- Metafield DTOs ----
public class MetafieldDto { public string? Id { get; set; } public string? Namespace { get; set; } public string? Key { get; set; } public string? Value { get; set; } public string? Type { get; set; } public string? Description { get; set; } public string? OwnerId { get; set; } public string? OwnerResource { get; set; } public string? CreatedAt { get; set; } public string? UpdatedAt { get; set; } }
public class ListMetafieldsResponse { public List<MetafieldDto>? Metafields { get; set; } }
public class MetafieldResponse { public MetafieldDto? Metafield { get; set; } }
public class CreateMetafieldRequest { [Required] public string Namespace { get; set; } = string.Empty; [Required] public string Key { get; set; } = string.Empty; [Required] public string Value { get; set; } = string.Empty; [Required] public string Type { get; set; } = "single_line_text_field"; public string? Description { get; set; } }
public class UpdateMetafieldRequest { public string? Value { get; set; } public string? Type { get; set; } }
