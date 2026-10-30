using ApiToolKit.Application.DTOs.Merchant.Program;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantProgramService : MerchantServiceBase, IMerchantProgramService
{
    public MerchantProgramService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetProgramAsync(GetProgramRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/programs/{Esc(r.ProgramId)}";
        return await SendAsync(r.AccountKey, "GetProgram", HttpMethod.Get, url);
    }

    public async Task<Result<object>> EnableProgramAsync(EnableProgramRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/programs/{Esc(r.ProgramId)}:enable";
        return await SendAsync(r.AccountKey, "EnableProgram", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> DisableProgramAsync(DisableProgramRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/programs/{Esc(r.ProgramId)}:disable";
        return await SendAsync(r.AccountKey, "DisableProgram", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ListProgramsAsync(ListProgramsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/programs";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListPrograms", HttpMethod.Get, url, query);
    }
}
