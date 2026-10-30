using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.BusinessProfile.Location;

/// <summary>List locations under an account.</summary>
public class ListLocationsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Comma-separated fields (default name,title,storefrontAddress,phoneNumbers,websiteUri,categories,regularHours)</summary>
    public string? ReadMask { get; set; }
    /// <summary>Filter expression</summary>
    public string? Filter { get; set; }
    /// <summary>Maximum results per page</summary>
    public int? PageSize { get; set; }
    /// <summary>Token for the next page</summary>
    public string? PageToken { get; set; }
}

/// <summary>Get one location.</summary>
public class GetLocationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Comma-separated fields (default name,title,storefrontAddress,phoneNumbers,websiteUri,categories,regularHours)</summary>
    public string? ReadMask { get; set; }
}

/// <summary>Create a new business location.</summary>
public class CreateLocationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Business name</summary>
    [Required] public string Title { get; set; } = string.Empty;
    /// <summary>e.g. en</summary>
    [Required] public string LanguageCode { get; set; } = string.Empty;
    /// <summary>Category name, e.g. categories/gcid:coffee_shop</summary>
    [Required] public string PrimaryCategory { get; set; } = string.Empty;
    /// <summary>PostalAddress object</summary>
    public object? StorefrontAddress { get; set; }
    /// <summary>PhoneNumbers object</summary>
    public object? PhoneNumbers { get; set; }
    /// <summary>Website URL</summary>
    public string? WebsiteUri { get; set; }
    /// <summary>BusinessHours object</summary>
    public object? RegularHours { get; set; }
    /// <summary>Validate without creating</summary>
    public bool? ValidateOnly { get; set; }
}

/// <summary>Update location fields listed in UpdateMask.</summary>
public class UpdateLocationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Comma-separated fields to update, e.g. title,websiteUri</summary>
    [Required] public string UpdateMask { get; set; } = string.Empty;
    /// <summary>Business name</summary>
    public string? Title { get; set; }
    /// <summary>Website URL</summary>
    public string? WebsiteUri { get; set; }
    /// <summary>PhoneNumbers object</summary>
    public object? PhoneNumbers { get; set; }
    /// <summary>BusinessHours object</summary>
    public object? RegularHours { get; set; }
    /// <summary>PostalAddress object</summary>
    public object? StorefrontAddress { get; set; }
}

/// <summary>Delete a location.</summary>
public class DeleteLocationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
}

/// <summary>Get attributes (amenities, services) of a location.</summary>
public class GetLocationAttributesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
}

/// <summary>Get the Google-updated version of a location (suggested edits).</summary>
public class GetGoogleUpdatedLocationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Fields to return</summary>
    public string? ReadMask { get; set; }
}

/// <summary>Update attributes of a location.</summary>
public class UpdateLocationAttributesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Comma-separated attribute names to update</summary>
    [Required] public string AttributeMask { get; set; } = string.Empty;
    /// <summary>Array of Attribute objects</summary>
    [Required] public object Attributes { get; set; } = new();
}

/// <summary>Get Google-updated attributes of a location.</summary>
public class GetGoogleUpdatedAttributesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
}

/// <summary>Move a location to another account.</summary>
public class TransferLocationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>accounts/{accountId}</summary>
    [Required] public string DestinationAccount { get; set; } = string.Empty;
}

/// <summary>List admins of a location.</summary>
public class ListLocationAdminsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
}

/// <summary>Invite a user to manage a location.</summary>
public class InviteLocationAdminRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Email address</summary>
    [Required] public string Admin { get; set; } = string.Empty;
    /// <summary>PRIMARY_OWNER, OWNER, MANAGER, SITE_MANAGER</summary>
    [Required] public string Role { get; set; } = string.Empty;
}

/// <summary>Change the role of a location admin.</summary>
public class UpdateLocationAdminRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Admin id</summary>
    [Required] public string AdminId { get; set; } = string.Empty;
    /// <summary>New role</summary>
    [Required] public string Role { get; set; } = string.Empty;
}

/// <summary>Remove an admin from a location.</summary>
public class RemoveLocationAdminRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Admin id</summary>
    [Required] public string AdminId { get; set; } = string.Empty;
}

/// <summary>Search Google Maps for existing locations that match a business.</summary>
public class SearchGoogleLocationsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Text query, e.g. business name and city</summary>
    public string? Query { get; set; }
    /// <summary>Location object to match</summary>
    public object? Location { get; set; }
    /// <summary>Max results (default 10)</summary>
    public int? PageSize { get; set; }
}

/// <summary>Get follower count of a location.</summary>
public class GetFollowersMetadataRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
}

/// <summary>Get food menus of a location.</summary>
public class GetFoodMenusRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
}

/// <summary>Replace food menus of a location.</summary>
public class UpdateFoodMenusRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Array of FoodMenu objects</summary>
    [Required] public object Menus { get; set; } = new();
    /// <summary>Fields to update</summary>
    public string? UpdateMask { get; set; }
}

/// <summary>Get the service list of a location.</summary>
public class GetServiceListRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
}

/// <summary>Replace the service list of a location.</summary>
public class UpdateServiceListRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Array of ServiceItem objects</summary>
    [Required] public object ServiceItems { get; set; } = new();
    /// <summary>Fields to update</summary>
    public string? UpdateMask { get; set; }
}

/// <summary>Get health provider attributes of a location.</summary>
public class GetHealthProviderAttributesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>e.g. en</summary>
    public string? LanguageCode { get; set; }
}

/// <summary>Update health provider attributes of a location.</summary>
public class UpdateHealthProviderAttributesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Array of InsuranceNetwork objects</summary>
    [Required] public object InsuranceNetworks { get; set; } = new();
    /// <summary>Fields to update</summary>
    public string? UpdateMask { get; set; }
}

/// <summary>List insurance networks Google supports for a location.</summary>
public class ListInsuranceNetworksRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>e.g. en</summary>
    public string? LanguageCode { get; set; }
    /// <summary>Maximum results per page</summary>
    public int? PageSize { get; set; }
    /// <summary>Token for the next page</summary>
    public string? PageToken { get; set; }
}

/// <summary>List Google locations recommended for the account to claim.</summary>
public class ListRecommendedGoogleLocationsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Maximum results per page</summary>
    public int? PageSize { get; set; }
    /// <summary>Token for the next page</summary>
    public string? PageToken { get; set; }
}
