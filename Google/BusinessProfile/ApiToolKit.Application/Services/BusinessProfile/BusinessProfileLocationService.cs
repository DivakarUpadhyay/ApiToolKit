using ApiToolKit.Application.DTOs.BusinessProfile.Location;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.BusinessProfile;

namespace ApiToolKit.Application.Services.BusinessProfile;

public class BusinessProfileLocationService : BusinessProfileServiceBase, IBusinessProfileLocationService
{
    public BusinessProfileLocationService(IBusinessProfileApiClient apiClient, IBusinessProfileAuthService authService, IBusinessProfileAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListLocationsAsync(ListLocationsRequest r)
    {
        var url = $"https://mybusinessbusinessinformation.googleapis.com/v1/accounts/{r.AccountId}/locations";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "readMask", r.ReadMask ?? "name,title,storefrontAddress,phoneNumbers,websiteUri,categories,regularHours");
        Add(query, "filter", r.Filter);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListLocations", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetLocationAsync(GetLocationRequest r)
    {
        var url = $"https://mybusinessbusinessinformation.googleapis.com/v1/locations/{r.LocationId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "readMask", r.ReadMask ?? "name,title,storefrontAddress,phoneNumbers,websiteUri,categories,regularHours");
        return await SendAsync(r.AccountKey, "GetLocation", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> CreateLocationAsync(CreateLocationRequest r)
    {
        var url = $"https://mybusinessbusinessinformation.googleapis.com/v1/accounts/{r.AccountId}/locations";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "validateOnly", r.ValidateOnly?.ToString().ToLower());
        return await SendAsync(r.AccountKey, "CreateLocation", HttpMethod.Post, url, query, new { title = r.Title, languageCode = r.LanguageCode, categories = new { primaryCategory = new { name = r.PrimaryCategory } }, storefrontAddress = r.StorefrontAddress, phoneNumbers = r.PhoneNumbers, websiteUri = r.WebsiteUri, regularHours = r.RegularHours });
    }

    public async Task<Result<object>> UpdateLocationAsync(UpdateLocationRequest r)
    {
        var url = $"https://mybusinessbusinessinformation.googleapis.com/v1/locations/{r.LocationId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateLocation", HttpMethod.Patch, url, query, new { title = r.Title, websiteUri = r.WebsiteUri, phoneNumbers = r.PhoneNumbers, regularHours = r.RegularHours, storefrontAddress = r.StorefrontAddress });
    }

    public async Task<Result<object>> DeleteLocationAsync(DeleteLocationRequest r)
    {
        var url = $"https://mybusinessbusinessinformation.googleapis.com/v1/locations/{r.LocationId}";
        return await SendAsync(r.AccountKey, "DeleteLocation", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> GetLocationAttributesAsync(GetLocationAttributesRequest r)
    {
        var url = $"https://mybusinessbusinessinformation.googleapis.com/v1/locations/{r.LocationId}/attributes";
        return await SendAsync(r.AccountKey, "GetLocationAttributes", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetGoogleUpdatedLocationAsync(GetGoogleUpdatedLocationRequest r)
    {
        var url = $"https://mybusinessbusinessinformation.googleapis.com/v1/locations/{r.LocationId}:getGoogleUpdated";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "readMask", r.ReadMask ?? "name,title,storefrontAddress,phoneNumbers,websiteUri,categories,regularHours");
        return await SendAsync(r.AccountKey, "GetGoogleUpdatedLocation", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UpdateLocationAttributesAsync(UpdateLocationAttributesRequest r)
    {
        var url = $"https://mybusinessbusinessinformation.googleapis.com/v1/locations/{r.LocationId}/attributes";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "attributeMask", r.AttributeMask);
        return await SendAsync(r.AccountKey, "UpdateLocationAttributes", HttpMethod.Patch, url, query, new { name = $"locations/{r.LocationId}/attributes", attributes = r.Attributes });
    }

    public async Task<Result<object>> GetGoogleUpdatedAttributesAsync(GetGoogleUpdatedAttributesRequest r)
    {
        var url = $"https://mybusinessbusinessinformation.googleapis.com/v1/locations/{r.LocationId}/attributes:getGoogleUpdated";
        return await SendAsync(r.AccountKey, "GetGoogleUpdatedAttributes", HttpMethod.Get, url);
    }

    public async Task<Result<object>> TransferLocationAsync(TransferLocationRequest r)
    {
        var url = $"https://mybusinessaccountmanagement.googleapis.com/v1/locations/{r.LocationId}:transfer";
        return await SendAsync(r.AccountKey, "TransferLocation", HttpMethod.Post, url, null, new { destinationAccount = r.DestinationAccount });
    }

    public async Task<Result<object>> ListLocationAdminsAsync(ListLocationAdminsRequest r)
    {
        var url = $"https://mybusinessaccountmanagement.googleapis.com/v1/locations/{r.LocationId}/admins";
        return await SendAsync(r.AccountKey, "ListLocationAdmins", HttpMethod.Get, url);
    }

    public async Task<Result<object>> InviteLocationAdminAsync(InviteLocationAdminRequest r)
    {
        var url = $"https://mybusinessaccountmanagement.googleapis.com/v1/locations/{r.LocationId}/admins";
        return await SendAsync(r.AccountKey, "InviteLocationAdmin", HttpMethod.Post, url, null, new { admin = r.Admin, role = r.Role });
    }

    public async Task<Result<object>> UpdateLocationAdminAsync(UpdateLocationAdminRequest r)
    {
        var url = $"https://mybusinessaccountmanagement.googleapis.com/v1/locations/{r.LocationId}/admins/{r.AdminId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", "role");
        return await SendAsync(r.AccountKey, "UpdateLocationAdmin", HttpMethod.Patch, url, query, new { role = r.Role });
    }

    public async Task<Result<object>> RemoveLocationAdminAsync(RemoveLocationAdminRequest r)
    {
        var url = $"https://mybusinessaccountmanagement.googleapis.com/v1/locations/{r.LocationId}/admins/{r.AdminId}";
        return await SendAsync(r.AccountKey, "RemoveLocationAdmin", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> SearchGoogleLocationsAsync(SearchGoogleLocationsRequest r)
    {
        var url = "https://mybusinessbusinessinformation.googleapis.com/v1/googleLocations:search";
        return await SendAsync(r.AccountKey, "SearchGoogleLocations", HttpMethod.Post, url, null, new { query = r.Query, location = r.Location, pageSize = r.PageSize });
    }

    public async Task<Result<object>> GetFollowersMetadataAsync(GetFollowersMetadataRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/followers/metadata";
        return await SendAsync(r.AccountKey, "GetFollowersMetadata", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetFoodMenusAsync(GetFoodMenusRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/foodMenus";
        return await SendAsync(r.AccountKey, "GetFoodMenus", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateFoodMenusAsync(UpdateFoodMenusRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/foodMenus";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateFoodMenus", HttpMethod.Patch, url, query, new { menus = r.Menus });
    }

    public async Task<Result<object>> GetServiceListAsync(GetServiceListRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/serviceList";
        return await SendAsync(r.AccountKey, "GetServiceList", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateServiceListAsync(UpdateServiceListRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/serviceList";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateServiceList", HttpMethod.Patch, url, query, new { serviceItems = r.ServiceItems });
    }

    public async Task<Result<object>> GetHealthProviderAttributesAsync(GetHealthProviderAttributesRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/healthProviderAttributes";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "languageCode", r.LanguageCode);
        return await SendAsync(r.AccountKey, "GetHealthProviderAttributes", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UpdateHealthProviderAttributesAsync(UpdateHealthProviderAttributesRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/healthProviderAttributes";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateHealthProviderAttributes", HttpMethod.Patch, url, query, new { insuranceNetworks = r.InsuranceNetworks });
    }

    public async Task<Result<object>> ListInsuranceNetworksAsync(ListInsuranceNetworksRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/insuranceNetworks";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "languageCode", r.LanguageCode);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListInsuranceNetworks", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> ListRecommendedGoogleLocationsAsync(ListRecommendedGoogleLocationsRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}:recommendGoogleLocations";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListRecommendedGoogleLocations", HttpMethod.Get, url, query);
    }
}
