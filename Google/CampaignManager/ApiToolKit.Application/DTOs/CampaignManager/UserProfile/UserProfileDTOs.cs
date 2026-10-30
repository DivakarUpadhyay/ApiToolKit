using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.UserProfile;

/// <summary>Retrieves list of user profiles for a user.</summary>
public class ListUserProfilesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
}

/// <summary>Gets one user profile by ID.</summary>
public class GetUserProfileRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>ProfileId (The user profile ID.)</summary>
    [Required] public string ProfileId { get; set; } = string.Empty;
}
