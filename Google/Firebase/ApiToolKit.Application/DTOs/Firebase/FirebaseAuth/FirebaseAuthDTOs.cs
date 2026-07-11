using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.Firebase.FirebaseAuth;

public class FirebaseSignUpRequest
{
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public string? DisplayName { get; set; }
    public bool ReturnSecureToken { get; set; } = true;
}

public class FirebaseSignInRequest
{
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public bool ReturnSecureToken { get; set; } = true;
}

public class FirebaseAuthUser
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
}

public class FirebaseProviderData
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

public class FirebaseUserRecord
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

    [JsonProperty("disabled")]
    public bool Disabled { get; set; }

    [JsonProperty("creationTime")]
    public string? CreationTime { get; set; }

    [JsonProperty("lastSignInTime")]
    public string? LastSignInTime { get; set; }

    [JsonProperty("validSince")]
    public string? ValidSince { get; set; }

    [JsonProperty("lastRefreshAt")]
    public string? LastRefreshAt { get; set; }

    [JsonProperty("passwordHash")]
    public string? PasswordHash { get; set; }

    [JsonProperty("passwordUpdatedAt")]
    public long PasswordUpdatedAt { get; set; }
}

public class GetFirebaseUserRequest
{
    public string? IdToken { get; set; }
    public string? LocalId { get; set; }
    public string? Email { get; set; }
}

public class GetFirebaseUserResponse
{
    [JsonProperty("users")]
    public List<FirebaseUserRecord>? Users { get; set; }
}

public class UpdateFirebaseUserRequest
{
    [Required]
    public string IdToken { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string? PhotoUrl { get; set; }
    public List<string> DeleteAttribute { get; set; } = new();
    public bool ReturnSecureToken { get; set; } = true;
}

public class AdminListUsersRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;
    public int MaxResults { get; set; } = 1000;
    public string? NextPageToken { get; set; }

    [Required]
    public string AccessToken { get; set; } = string.Empty;
}

public class AdminListUsersResponse
{
    [JsonProperty("users")]
    public List<FirebaseUserRecord>? Users { get; set; }

    [JsonProperty("nextPageToken")]
    public string? NextPageToken { get; set; }
}

public class AdminCreateUserRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public string? DisplayName { get; set; }
    public string? PhotoUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public bool EmailVerified { get; set; } = false;
    public bool Disabled { get; set; } = false;

    [Required]
    public string AccessToken { get; set; } = string.Empty;
}

public class AdminUpdateUserRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string LocalId { get; set; } = string.Empty;

    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? DisplayName { get; set; }
    public string? PhotoUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public bool? EmailVerified { get; set; }
    public bool? Disabled { get; set; }

    [Required]
    public string AccessToken { get; set; } = string.Empty;
}

public class AdminDeleteUserRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string LocalId { get; set; } = string.Empty;

    [Required]
    public string AccessToken { get; set; } = string.Empty;
}

public class VerifyIdTokenRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string IdToken { get; set; } = string.Empty;

    [Required]
    public string AccessToken { get; set; } = string.Empty;
}

public class CreateCustomTokenRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string Uid { get; set; } = string.Empty;

    public Dictionary<string, object>? Claims { get; set; }

    [Required]
    public string AccessToken { get; set; } = string.Empty;
}

public class CustomTokenResponse
{
    public string? CustomToken { get; set; }
}
