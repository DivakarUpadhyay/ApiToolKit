using ApiToolKit.Application.DTOs.Identity.Toolkit;
using ApiToolKit.Application.Interfaces.Services.Identity;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Identity;
using ApiToolKit.Domain.Interfaces.Repositories.Identity;

namespace ApiToolKit.Application.Services.Identity;

public class IdentityToolkitService : IIdentityToolkitService
{
    private readonly IIdentityApiClient _apiClient;
    private readonly IIdentityAuthRepository _authRepo;
    private readonly IIdentityUserRepository _userRepo;

    public IdentityToolkitService(IIdentityApiClient apiClient, IIdentityAuthRepository authRepo, IIdentityUserRepository userRepo)
    {
        _apiClient = apiClient;
        _authRepo = authRepo;
        _userRepo = userRepo;
    }

    private async Task<string> GetApiKeyAsync()
    {
        var config = await _authRepo.GetConfigAsync();
        return config?.ApiKey ?? throw new InvalidOperationException("Identity API key not configured.");
    }

    private string ToolkitUrl(string endpoint, string apiKey) =>
        $"https://identitytoolkit.googleapis.com/v1/accounts:{endpoint}?key={apiKey}";

    public async Task<Result<AuthUser>> SignUpAsync(SignUpRequest request)
    {
        try
        {
            var apiKey = await GetApiKeyAsync();
            var response = await _apiClient.PostAsync<AuthUser>(ToolkitUrl("signUp", apiKey), request);
            if (response == null) return Result<AuthUser>.Fail("No response from Identity Toolkit.");

            if (!string.IsNullOrEmpty(response.LocalId))
            {
                var user = new IdentityUser
                {
                    LocalId = response.LocalId,
                    Email = response.Email,
                    DisplayName = response.DisplayName,
                    LocalCreatedAt = DateTime.UtcNow
                };
                await _userRepo.SaveUserAsync(user);
            }

            await _authRepo.SaveApiLogAsync(new IdentityApiLog
            {
                UserId = response.LocalId,
                ApiName = "SignUp",
                RequestBody = $"email={request.Email}",
                ResponseBody = $"localId={response.LocalId}",
                IsSuccess = true,
                StatusCode = 200,
                CreatedAt = DateTime.UtcNow
            });

            return Result<AuthUser>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<AuthUser>.Fail($"SignUp failed: {ex.Message}");
        }
    }

    public async Task<Result<AuthUser>> SignInAsync(SignInRequest request)
    {
        try
        {
            var apiKey = await GetApiKeyAsync();
            var response = await _apiClient.PostAsync<AuthUser>(ToolkitUrl("signInWithPassword", apiKey), request);
            if (response == null) return Result<AuthUser>.Fail("No response from Identity Toolkit.");
            return Result<AuthUser>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<AuthUser>.Fail($"SignIn failed: {ex.Message}");
        }
    }

    public async Task<Result<GetAccountInfoResponse>> GetAccountInfoAsync(GetAccountInfoRequest request)
    {
        try
        {
            var apiKey = await GetApiKeyAsync();
            var response = await _apiClient.PostAsync<GetAccountInfoResponse>(
                ToolkitUrl("lookup", apiKey), new { idToken = request.IdToken });
            if (response == null) return Result<GetAccountInfoResponse>.Fail("No response from Identity Toolkit.");
            return Result<GetAccountInfoResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetAccountInfoResponse>.Fail($"GetAccountInfo failed: {ex.Message}");
        }
    }

    public async Task<Result<AuthUser>> UpdateProfileAsync(UpdateProfileRequest request)
    {
        try
        {
            var apiKey = await GetApiKeyAsync();
            var body = new
            {
                idToken = request.IdToken,
                displayName = request.DisplayName,
                photoUrl = request.PhotoUrl,
                deleteAttribute = request.DeleteAttribute,
                returnSecureToken = request.ReturnSecureToken
            };
            var response = await _apiClient.PostAsync<AuthUser>(ToolkitUrl("update", apiKey), body);
            if (response == null) return Result<AuthUser>.Fail("No response from Identity Toolkit.");

            if (!string.IsNullOrEmpty(response.LocalId))
            {
                var user = new IdentityUser
                {
                    LocalId = response.LocalId,
                    Email = response.Email,
                    DisplayName = response.DisplayName,
                    LocalCreatedAt = DateTime.UtcNow
                };
                await _userRepo.UpdateUserAsync(user);
            }

            return Result<AuthUser>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<AuthUser>.Fail($"UpdateProfile failed: {ex.Message}");
        }
    }

    public async Task<Result<AuthUser>> ChangePasswordAsync(ChangePasswordRequest request)
    {
        try
        {
            var apiKey = await GetApiKeyAsync();
            var body = new
            {
                idToken = request.IdToken,
                password = request.Password,
                returnSecureToken = request.ReturnSecureToken
            };
            var response = await _apiClient.PostAsync<AuthUser>(ToolkitUrl("update", apiKey), body);
            if (response == null) return Result<AuthUser>.Fail("No response from Identity Toolkit.");
            return Result<AuthUser>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<AuthUser>.Fail($"ChangePassword failed: {ex.Message}");
        }
    }

    public async Task<Result<AuthUser>> ChangeEmailAsync(ChangeEmailRequest request)
    {
        try
        {
            var apiKey = await GetApiKeyAsync();
            var body = new
            {
                idToken = request.IdToken,
                email = request.Email,
                returnSecureToken = request.ReturnSecureToken
            };
            var response = await _apiClient.PostAsync<AuthUser>(ToolkitUrl("update", apiKey), body);
            if (response == null) return Result<AuthUser>.Fail("No response from Identity Toolkit.");
            return Result<AuthUser>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<AuthUser>.Fail($"ChangeEmail failed: {ex.Message}");
        }
    }

    public async Task<Result<OobCodeResponse>> SendVerificationEmailAsync(VerifyEmailRequest request)
    {
        try
        {
            var apiKey = await GetApiKeyAsync();
            var body = new { requestType = request.RequestType, idToken = request.IdToken };
            var response = await _apiClient.PostAsync<OobCodeResponse>(ToolkitUrl("sendOobCode", apiKey), body);
            if (response == null) return Result<OobCodeResponse>.Fail("No response from Identity Toolkit.");
            return Result<OobCodeResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<OobCodeResponse>.Fail($"SendVerificationEmail failed: {ex.Message}");
        }
    }

    public async Task<Result<OobCodeResponse>> SendPasswordResetAsync(ResetPasswordRequest request)
    {
        try
        {
            var apiKey = await GetApiKeyAsync();
            var body = new { requestType = request.RequestType, email = request.Email };
            var response = await _apiClient.PostAsync<OobCodeResponse>(ToolkitUrl("sendOobCode", apiKey), body);
            if (response == null) return Result<OobCodeResponse>.Fail("No response from Identity Toolkit.");
            return Result<OobCodeResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<OobCodeResponse>.Fail($"SendPasswordReset failed: {ex.Message}");
        }
    }

    public async Task<Result<bool>> DeleteAccountAsync(DeleteAccountRequest request)
    {
        try
        {
            var apiKey = await GetApiKeyAsync();
            var body = new { idToken = request.IdToken };
            await _apiClient.PostAsync<object>(ToolkitUrl("delete", apiKey), body);
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Fail($"DeleteAccount failed: {ex.Message}");
        }
    }
}
