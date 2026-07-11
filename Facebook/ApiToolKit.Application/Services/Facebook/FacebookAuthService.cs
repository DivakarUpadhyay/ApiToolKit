using ApiToolKit.Application.DTOs.Facebook.Auth;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookAuthService : IFacebookAuthService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookAuthService(IFacebookAuthRepository authRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<GetAuthUrlResponse>> GetAuthUrlAsync(GetAuthUrlRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<GetAuthUrlResponse>.Fail("Facebook configuration not found.");

        var scopes = string.Join(",", request.Scope ?? new List<string> { "public_profile", "email" });
        var baseDialogUrl = config.BaseUrl.Replace("graph.facebook.com", "www.facebook.com");
        var url = $"{baseDialogUrl}/dialog/oauth?client_id={config.AppId}&redirect_uri={Uri.EscapeDataString(config.RedirectUrl)}&scope={scopes}&response_type=code";
        if (!string.IsNullOrEmpty(request.State))
            url += $"&state={Uri.EscapeDataString(request.State)}";

        await _authRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "GetAuthUrl", IsSuccess = true, StatusCode = 200 });
        return Result<GetAuthUrlResponse>.Ok(new GetAuthUrlResponse { AuthUrl = url });
    }

    public async Task<Result<ExchangeCodeResponse>> ExchangeCodeAsync(ExchangeCodeRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ExchangeCodeResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["client_id"] = config.AppId,
            ["client_secret"] = config.AppSecret,
            ["redirect_uri"] = config.RedirectUrl,
            ["code"] = request.Code
        };

        var response = await _apiClient.GetAsync<dynamic>($"/oauth/access_token", queryParams);
        if (response == null)
            return Result<ExchangeCodeResponse>.Fail("No response from Facebook API.");

        string accessToken = response?.access_token?.ToString() ?? string.Empty;
        string tokenType = response?.token_type?.ToString() ?? "bearer";
        int expiresIn = 0;
        if (response?.expires_in != null) int.TryParse(response.expires_in.ToString(), out expiresIn);

        await _authRepo.SaveTokenAsync(new FacebookToken
        {
            AccessToken = accessToken,
            TokenType = tokenType,
            ExpiresAt = expiresIn > 0 ? DateTime.UtcNow.AddSeconds(expiresIn) : null,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        await _authRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "ExchangeCode",
            RequestBody = JsonConvert.SerializeObject(new { code = request.Code }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<ExchangeCodeResponse>.Ok(new ExchangeCodeResponse
        {
            AccessToken = accessToken,
            TokenType = tokenType,
            ExpiresIn = expiresIn
        });
    }

    public async Task<Result<GetAppTokenResponse>> GetAppAccessTokenAsync()
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<GetAppTokenResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["client_id"] = config.AppId,
            ["client_secret"] = config.AppSecret,
            ["grant_type"] = "client_credentials"
        };

        var response = await _apiClient.GetAsync<dynamic>($"/oauth/access_token", queryParams);
        if (response == null)
            return Result<GetAppTokenResponse>.Fail("No response from Facebook API.");

        await _authRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "GetAppAccessToken",
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<GetAppTokenResponse>.Ok(new GetAppTokenResponse
        {
            AccessToken = response?.access_token?.ToString() ?? string.Empty,
            TokenType = response?.token_type?.ToString() ?? "bearer"
        });
    }

    public async Task<Result<LongLivedTokenResponse>> GetLongLivedTokenAsync(LongLivedTokenRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<LongLivedTokenResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["grant_type"] = "fb_exchange_token",
            ["client_id"] = config.AppId,
            ["client_secret"] = config.AppSecret,
            ["fb_exchange_token"] = request.ShortLivedToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/oauth/access_token", queryParams);
        if (response == null)
            return Result<LongLivedTokenResponse>.Fail("No response from Facebook API.");

        string accessToken = response?.access_token?.ToString() ?? string.Empty;
        int expiresIn = 0;
        if (response?.expires_in != null) int.TryParse(response.expires_in.ToString(), out expiresIn);

        await _authRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "GetLongLivedToken",
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<LongLivedTokenResponse>.Ok(new LongLivedTokenResponse
        {
            AccessToken = accessToken,
            TokenType = response?.token_type?.ToString() ?? "bearer",
            ExpiresIn = expiresIn
        });
    }

    public async Task<Result<DebugTokenResponse>> DebugTokenAsync(DebugTokenRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<DebugTokenResponse>.Fail("Facebook configuration not found.");

        var appToken = $"{config.AppId}|{config.AppSecret}";
        var queryParams = new Dictionary<string, string>
        {
            ["input_token"] = request.InputToken,
            ["access_token"] = appToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/debug_token", queryParams);
        if (response == null)
            return Result<DebugTokenResponse>.Fail("No response from Facebook API.");

        var data = response?.data;
        bool isValid = data?.is_valid == true;

        await _authRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "DebugToken",
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<DebugTokenResponse>.Ok(new DebugTokenResponse
        {
            AppId = data?.app_id?.ToString() ?? string.Empty,
            Type = data?.type?.ToString() ?? string.Empty,
            UserId = data?.user_id?.ToString() ?? string.Empty,
            IsValid = isValid,
            ExpiresAt = data?.expires_at ?? 0,
            Scopes = data?.scopes != null
                ? JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(data.scopes)) ?? new List<string>()
                : new List<string>()
        });
    }

    public async Task<Result<LongLivedTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<LongLivedTokenResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["grant_type"] = "fb_exchange_token",
            ["client_id"] = config.AppId,
            ["client_secret"] = config.AppSecret,
            ["fb_exchange_token"] = request.AccessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/oauth/access_token", queryParams);
        if (response == null)
            return Result<LongLivedTokenResponse>.Fail("No response from Facebook API.");

        string accessToken = response?.access_token?.ToString() ?? string.Empty;
        int expiresIn = 0;
        if (response?.expires_in != null) int.TryParse(response.expires_in.ToString(), out expiresIn);

        await _authRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "RefreshToken",
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<LongLivedTokenResponse>.Ok(new LongLivedTokenResponse
        {
            AccessToken = accessToken,
            TokenType = response?.token_type?.ToString() ?? "bearer",
            ExpiresIn = expiresIn
        });
    }
}
