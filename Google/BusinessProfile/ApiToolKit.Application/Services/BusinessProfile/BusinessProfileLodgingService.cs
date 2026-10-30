using ApiToolKit.Application.DTOs.BusinessProfile.Lodging;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.BusinessProfile;

namespace ApiToolKit.Application.Services.BusinessProfile;

public class BusinessProfileLodgingService : BusinessProfileServiceBase, IBusinessProfileLodgingService
{
    public BusinessProfileLodgingService(IBusinessProfileApiClient apiClient, IBusinessProfileAuthService authService, IBusinessProfileAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetLodgingAsync(GetLodgingRequest r)
    {
        var url = $"https://mybusinesslodging.googleapis.com/v1/locations/{r.LocationId}/lodging";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "readMask", r.ReadMask ?? "*");
        return await SendAsync(r.AccountKey, "GetLodging", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UpdateLodgingAsync(UpdateLodgingRequest r)
    {
        var url = $"https://mybusinesslodging.googleapis.com/v1/locations/{r.LocationId}/lodging";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateLodging", HttpMethod.Patch, url, query, r.Lodging);
    }

    public async Task<Result<object>> GetGoogleUpdatedLodgingAsync(GetGoogleUpdatedLodgingRequest r)
    {
        var url = $"https://mybusinesslodging.googleapis.com/v1/locations/{r.LocationId}/lodging:getGoogleUpdated";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "readMask", r.ReadMask ?? "*");
        return await SendAsync(r.AccountKey, "GetGoogleUpdatedLodging", HttpMethod.Get, url, query);
    }
}
