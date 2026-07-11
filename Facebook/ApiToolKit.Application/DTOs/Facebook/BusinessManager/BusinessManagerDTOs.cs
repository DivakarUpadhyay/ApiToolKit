using Newtonsoft.Json;
namespace ApiToolKit.Application.DTOs.Facebook.BusinessManager;

public record Business(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("verification_status")] string? VerificationStatus,
    [property:JsonProperty("link")] string? Link,
    [property:JsonProperty("primary_page")] BusinessPageRef? PrimaryPage);

public record BusinessPageRef([property:JsonProperty("id")] string? Id, [property:JsonProperty("name")] string? Name);

public record BusinessPage(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("category")] string? Category,
    [property:JsonProperty("link")] string? Link,
    [property:JsonProperty("access_status")] string? AccessStatus);

public record BusinessPagesResponse([property:JsonProperty("data")] List<BusinessPage>? Data);

public record BusinessAdAccount(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("account_status")] int? AccountStatus,
    [property:JsonProperty("currency")] string? Currency,
    [property:JsonProperty("timezone_name")] string? TimezoneName);

public record BusinessAdAccountsResponse([property:JsonProperty("data")] List<BusinessAdAccount>? Data);

public record BusinessInstagramAccount(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("username")] string? Username,
    [property:JsonProperty("name")] string? Name);

public record BusinessInstagramAccountsResponse([property:JsonProperty("data")] List<BusinessInstagramAccount>? Data);

public record BusinessCatalog(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("vertical")] string? Vertical);

public record BusinessCatalogsResponse([property:JsonProperty("data")] List<BusinessCatalog>? Data);

public record BusinessUser(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("email")] string? Email,
    [property:JsonProperty("role")] string? Role,
    [property:JsonProperty("status")] string? Status);

public record BusinessUsersResponse([property:JsonProperty("data")] List<BusinessUser>? Data);
public record InviteBusinessUserRequest(string Email, string Role);

public record SystemUser(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("role")] string? Role);

public record SystemUsersResponse([property:JsonProperty("data")] List<SystemUser>? Data);
public record CreateSystemUserRequest(string Name, string Role);
public record ClaimPageRequest(string PageId);
