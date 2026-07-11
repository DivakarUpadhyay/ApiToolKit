using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.Identity.Toolkit;

public class SignUpRequest
{
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public string? DisplayName { get; set; }
    public bool ReturnSecureToken { get; set; } = true;
}

public class SignInRequest
{
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public bool ReturnSecureToken { get; set; } = true;
}

public class AuthUser
{
    [JsonProperty("idToken")]
    public string? IdToken { get; set; }

    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("refreshToken")]
    public string? RefreshToken { get; set; }

    [JsonProperty("expiresIn")]
    public string? ExpiresIn { get; set; }

    [JsonProperty("localId")]
    public string? LocalId { get; set; }

    [JsonProperty("displayName")]
    public string? DisplayName { get; set; }

    [JsonProperty("registered")]
    public bool? Registered { get; set; }
}

public class GetAccountInfoRequest
{
    [Required]
    public string IdToken { get; set; } = string.Empty;
}

public class ProviderUserInfoDto
{
    [JsonProperty("providerId")]
    public string? ProviderId { get; set; }

    [JsonProperty("displayName")]
    public string? DisplayName { get; set; }

    [JsonProperty("photoUrl")]
    public string? PhotoUrl { get; set; }

    [JsonProperty("federatedId")]
    public string? FederatedId { get; set; }

    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("rawId")]
    public string? RawId { get; set; }

    [JsonProperty("screenName")]
    public string? ScreenName { get; set; }
}

public class UserInfo
{
    [JsonProperty("localId")]
    public string? LocalId { get; set; }

    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("emailVerified")]
    public bool EmailVerified { get; set; }

    [JsonProperty("displayName")]
    public string? DisplayName { get; set; }

    [JsonProperty("photoUrl")]
    public string? PhotoUrl { get; set; }

    [JsonProperty("phoneNumber")]
    public string? PhoneNumber { get; set; }

    [JsonProperty("providerUserInfo")]
    public List<ProviderUserInfoDto>? ProviderUserInfo { get; set; }

    [JsonProperty("passwordHash")]
    public string? PasswordHash { get; set; }

    [JsonProperty("passwordUpdatedAt")]
    public long PasswordUpdatedAt { get; set; }

    [JsonProperty("validSince")]
    public string? ValidSince { get; set; }

    [JsonProperty("disabled")]
    public bool Disabled { get; set; }

    [JsonProperty("lastLoginAt")]
    public string? LastLoginAt { get; set; }

    [JsonProperty("createdAt")]
    public string? CreatedAt { get; set; }

    [JsonProperty("lastRefreshAt")]
    public string? LastRefreshAt { get; set; }
}

public class GetAccountInfoResponse
{
    [JsonProperty("users")]
    public List<UserInfo>? Users { get; set; }
}

public class UpdateProfileRequest
{
    [Required]
    public string IdToken { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string? PhotoUrl { get; set; }
    public List<string>? DeleteAttribute { get; set; }
    public bool ReturnSecureToken { get; set; } = true;
}

public class ChangePasswordRequest
{
    [Required]
    public string IdToken { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public bool ReturnSecureToken { get; set; } = true;
}

public class ChangeEmailRequest
{
    [Required]
    public string IdToken { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    public bool ReturnSecureToken { get; set; } = true;
}

public class VerifyEmailRequest
{
    public string RequestType { get; set; } = "VERIFY_EMAIL";

    [Required]
    public string IdToken { get; set; } = string.Empty;
}

public class ResetPasswordRequest
{
    public string RequestType { get; set; } = "PASSWORD_RESET";

    [Required]
    public string Email { get; set; } = string.Empty;
}

public class DeleteAccountRequest
{
    [Required]
    public string IdToken { get; set; } = string.Empty;
}

public class OobCodeResponse
{
    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("kind")]
    public string? Kind { get; set; }
}

public class ListUsersRequest
{
    public int PageSize { get; set; } = 1000;
    public string? NextPageToken { get; set; }
}

public class ListUsersResponse
{
    [JsonProperty("users")]
    public List<UserInfo>? Users { get; set; }

    [JsonProperty("nextPageToken")]
    public string? NextPageToken { get; set; }
}

public class AdminGetUserRequest
{
    public string? LocalId { get; set; }
    public string? Email { get; set; }
}

public class AdminUpdateUserRequest
{
    [Required]
    public string LocalId { get; set; } = string.Empty;
    public string? Email { get; set; }
    public bool? EmailVerified { get; set; }
    public string? DisplayName { get; set; }
    public string? PhotoUrl { get; set; }
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }
    public bool? Disabled { get; set; }
}

public class AdminDeleteUserRequest
{
    [Required]
    public string LocalId { get; set; } = string.Empty;
}

public class AdminCreateUserRequest
{
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public string? DisplayName { get; set; }
    public string? PhotoUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public bool EmailVerified { get; set; } = false;
    public bool Disabled { get; set; } = false;
}
