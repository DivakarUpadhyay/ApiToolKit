using ApiToolKit.Application.DTOs.Firebase.FirebaseAuth;
using ApiToolKit.Application.Interfaces.Services.Firebase;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Firebase;
using ApiToolKit.Domain.Interfaces.Repositories.Firebase;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Firebase;

public class FirebaseUserService : IFirebaseUserService
{
    private readonly IFirebaseApiClient _apiClient;
    private readonly IFirebaseAuthRepository _authRepo;
    private readonly IFirebaseUserRepository _userRepo;

    public FirebaseUserService(IFirebaseApiClient apiClient, IFirebaseAuthRepository authRepo, IFirebaseUserRepository userRepo)
    {
        _apiClient = apiClient;
        _authRepo = authRepo;
        _userRepo = userRepo;
    }

    private async Task<(string apiKey, string projectId)> GetConfigAsync()
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) throw new InvalidOperationException("Firebase config not found.");
        // ApiKey is stored in appsettings; retrieve from config or environment
        return (string.Empty, config.ProjectId);
    }

    private string AuthUrl(string endpoint, string apiKey) =>
        $"https://identitytoolkit.googleapis.com/v1/accounts:{endpoint}?key={apiKey}";

    private string AdminUrl(string projectId, string path, string apiKey) =>
        $"https://identitytoolkit.googleapis.com/v1/projects/{projectId}/{path}?key={apiKey}";

    public async Task<Result<FirebaseAuthUser>> SignUpAsync(FirebaseSignUpRequest request)
    {
        try
        {
            // Use direct API call via ApiClient — apiKey embedded in URL
            var response = await _apiClient.PostAsync<FirebaseAuthUser>(
                $"https://identitytoolkit.googleapis.com/v1/accounts:signUp", request);
            if (response == null) return Result<FirebaseAuthUser>.Fail("No response from Firebase Auth.");

            if (!string.IsNullOrEmpty(response.LocalId))
            {
                var config = await _authRepo.GetConfigAsync();
                await _userRepo.SaveUserAsync(new FirebaseUser
                {
                    Uid = response.LocalId,
                    ProjectId = config?.ProjectId ?? string.Empty,
                    Email = response.Email,
                    DisplayName = response.DisplayName,
                    CreatedAt = DateTime.UtcNow
                });
            }

            return Result<FirebaseAuthUser>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<FirebaseAuthUser>.Fail($"SignUp failed: {ex.Message}");
        }
    }

    public async Task<Result<FirebaseAuthUser>> SignInAsync(FirebaseSignInRequest request)
    {
        try
        {
            var response = await _apiClient.PostAsync<FirebaseAuthUser>(
                $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword", request);
            if (response == null) return Result<FirebaseAuthUser>.Fail("No response from Firebase Auth.");
            return Result<FirebaseAuthUser>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<FirebaseAuthUser>.Fail($"SignIn failed: {ex.Message}");
        }
    }

    public async Task<Result<GetFirebaseUserResponse>> GetUserAsync(GetFirebaseUserRequest request)
    {
        try
        {
            object body;
            if (!string.IsNullOrEmpty(request.IdToken))
                body = new { idToken = request.IdToken };
            else if (!string.IsNullOrEmpty(request.LocalId))
                body = new { localId = new[] { request.LocalId } };
            else
                body = new { email = new[] { request.Email } };

            var response = await _apiClient.PostAsync<GetFirebaseUserResponse>(
                $"https://identitytoolkit.googleapis.com/v1/accounts:lookup", body);
            if (response == null) return Result<GetFirebaseUserResponse>.Fail("No response from Firebase Auth.");
            return Result<GetFirebaseUserResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetFirebaseUserResponse>.Fail($"GetUser failed: {ex.Message}");
        }
    }

    public async Task<Result<FirebaseAuthUser>> UpdateUserAsync(UpdateFirebaseUserRequest request)
    {
        try
        {
            var body = new
            {
                idToken = request.IdToken,
                displayName = request.DisplayName,
                photoUrl = request.PhotoUrl,
                deleteAttribute = request.DeleteAttribute,
                returnSecureToken = request.ReturnSecureToken
            };
            var response = await _apiClient.PostAsync<FirebaseAuthUser>(
                $"https://identitytoolkit.googleapis.com/v1/accounts:update", body);
            if (response == null) return Result<FirebaseAuthUser>.Fail("No response from Firebase Auth.");
            return Result<FirebaseAuthUser>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<FirebaseAuthUser>.Fail($"UpdateUser failed: {ex.Message}");
        }
    }

    public async Task<Result<AdminListUsersResponse>> AdminListUsersAsync(AdminListUsersRequest request)
    {
        try
        {
            var body = new { maxResults = request.MaxResults, nextPageToken = request.NextPageToken };
            var response = await _apiClient.PostAsync<AdminListUsersResponse>(
                $"https://identitytoolkit.googleapis.com/v1/projects/{request.ProjectId}/accounts:batchGet",
                body, request.AccessToken);
            if (response == null) return Result<AdminListUsersResponse>.Fail("No response from Admin API.");
            return Result<AdminListUsersResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<AdminListUsersResponse>.Fail($"AdminListUsers failed: {ex.Message}");
        }
    }

    public async Task<Result<FirebaseUserRecord>> AdminCreateUserAsync(AdminCreateUserRequest request)
    {
        try
        {
            var body = new
            {
                email = request.Email,
                password = request.Password,
                displayName = request.DisplayName,
                photoUrl = request.PhotoUrl,
                phoneNumber = request.PhoneNumber,
                emailVerified = request.EmailVerified,
                disabled = request.Disabled
            };
            var response = await _apiClient.PostAsync<FirebaseUserRecord>(
                $"https://identitytoolkit.googleapis.com/v1/projects/{request.ProjectId}/accounts",
                body, request.AccessToken);
            if (response == null) return Result<FirebaseUserRecord>.Fail("No response from Admin API.");

            await _userRepo.SaveUserAsync(new FirebaseUser
            {
                Uid = response.LocalId ?? string.Empty,
                ProjectId = request.ProjectId,
                Email = response.Email,
                DisplayName = response.DisplayName,
                EmailVerified = response.EmailVerified,
                Disabled = response.Disabled,
                CreatedAt = DateTime.UtcNow
            });

            return Result<FirebaseUserRecord>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<FirebaseUserRecord>.Fail($"AdminCreateUser failed: {ex.Message}");
        }
    }

    public async Task<Result<FirebaseUserRecord>> AdminUpdateUserAsync(AdminUpdateUserRequest request)
    {
        try
        {
            var body = new
            {
                localId = request.LocalId,
                email = request.Email,
                password = request.Password,
                displayName = request.DisplayName,
                photoUrl = request.PhotoUrl,
                phoneNumber = request.PhoneNumber,
                emailVerified = request.EmailVerified,
                disableUser = request.Disabled
            };
            var response = await _apiClient.PostAsync<FirebaseUserRecord>(
                $"https://identitytoolkit.googleapis.com/v1/projects/{request.ProjectId}/accounts:update",
                body, request.AccessToken);
            if (response == null) return Result<FirebaseUserRecord>.Fail("No response from Admin API.");
            return Result<FirebaseUserRecord>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<FirebaseUserRecord>.Fail($"AdminUpdateUser failed: {ex.Message}");
        }
    }

    public async Task<Result<bool>> AdminDeleteUserAsync(AdminDeleteUserRequest request)
    {
        try
        {
            var body = new { localId = request.LocalId };
            await _apiClient.PostAsync<object>(
                $"https://identitytoolkit.googleapis.com/v1/projects/{request.ProjectId}/accounts:delete",
                body, request.AccessToken);
            await _userRepo.DeleteUserAsync(request.LocalId, request.ProjectId);
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Fail($"AdminDeleteUser failed: {ex.Message}");
        }
    }

    public async Task<Result<Dictionary<string, object>>> VerifyIdTokenAsync(VerifyIdTokenRequest request)
    {
        try
        {
            var body = new { idToken = request.IdToken };
            var response = await _apiClient.PostAsync<Dictionary<string, object>>(
                $"https://identitytoolkit.googleapis.com/v1/projects/{request.ProjectId}/accounts:lookup",
                body, request.AccessToken);
            if (response == null) return Result<Dictionary<string, object>>.Fail("No response from Admin API.");
            return Result<Dictionary<string, object>>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<Dictionary<string, object>>.Fail($"VerifyIdToken failed: {ex.Message}");
        }
    }

    public async Task<Result<CustomTokenResponse>> CreateCustomTokenAsync(CreateCustomTokenRequest request)
    {
        try
        {
            var body = new
            {
                uid = request.Uid,
                claims = request.Claims
            };
            var response = await _apiClient.PostAsync<CustomTokenResponse>(
                $"https://identitytoolkit.googleapis.com/v1/projects/{request.ProjectId}:createCustomToken",
                body, request.AccessToken);
            if (response == null) return Result<CustomTokenResponse>.Fail("No response from Admin API.");
            return Result<CustomTokenResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<CustomTokenResponse>.Fail($"CreateCustomToken failed: {ex.Message}");
        }
    }
}
