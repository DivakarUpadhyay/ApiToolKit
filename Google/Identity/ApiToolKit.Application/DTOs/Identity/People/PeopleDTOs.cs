using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.Identity.People;

public class FieldMetadata
{
    [JsonProperty("primary")]
    public bool Primary { get; set; }

    [JsonProperty("verified")]
    public bool Verified { get; set; }

    [JsonProperty("source")]
    public object? Source { get; set; }
}

public class PersonNameDto
{
    [JsonProperty("displayName")]
    public string? DisplayName { get; set; }

    [JsonProperty("familyName")]
    public string? FamilyName { get; set; }

    [JsonProperty("givenName")]
    public string? GivenName { get; set; }

    [JsonProperty("honorificPrefix")]
    public string? HonorificPrefix { get; set; }

    [JsonProperty("honorificSuffix")]
    public string? HonorificSuffix { get; set; }

    [JsonProperty("displayNameLastFirst")]
    public string? DisplayNameLastFirst { get; set; }
}

public class EmailAddressDto
{
    [JsonProperty("value")]
    public string? Value { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("formattedType")]
    public string? FormattedType { get; set; }

    [JsonProperty("metadata")]
    public FieldMetadata? Metadata { get; set; }
}

public class PhoneNumberDto
{
    [JsonProperty("value")]
    public string? Value { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("formattedType")]
    public string? FormattedType { get; set; }

    [JsonProperty("canonicalForm")]
    public string? CanonicalForm { get; set; }

    [JsonProperty("metadata")]
    public FieldMetadata? Metadata { get; set; }
}

public class PhotoDto
{
    [JsonProperty("url")]
    public string? Url { get; set; }

    [JsonProperty("default")]
    public bool Default { get; set; }

    [JsonProperty("metadata")]
    public FieldMetadata? Metadata { get; set; }
}

public class OrganizationDto
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("department")]
    public string? Department { get; set; }

    [JsonProperty("location")]
    public string? Location { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("current")]
    public bool Current { get; set; }

    [JsonProperty("formattedType")]
    public string? FormattedType { get; set; }

    [JsonProperty("metadata")]
    public FieldMetadata? Metadata { get; set; }
}

public class AddressDto
{
    [JsonProperty("formattedValue")]
    public string? FormattedValue { get; set; }

    [JsonProperty("streetAddress")]
    public string? StreetAddress { get; set; }

    [JsonProperty("city")]
    public string? City { get; set; }

    [JsonProperty("region")]
    public string? Region { get; set; }

    [JsonProperty("postalCode")]
    public string? PostalCode { get; set; }

    [JsonProperty("country")]
    public string? Country { get; set; }

    [JsonProperty("countryCode")]
    public string? CountryCode { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("formattedType")]
    public string? FormattedType { get; set; }

    [JsonProperty("metadata")]
    public FieldMetadata? Metadata { get; set; }
}

public class UrlDto
{
    [JsonProperty("value")]
    public string? Value { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("formattedType")]
    public string? FormattedType { get; set; }

    [JsonProperty("metadata")]
    public FieldMetadata? Metadata { get; set; }
}

public class BiographyDto
{
    [JsonProperty("value")]
    public string? Value { get; set; }

    [JsonProperty("contentType")]
    public string? ContentType { get; set; }

    [JsonProperty("metadata")]
    public FieldMetadata? Metadata { get; set; }
}

public class PersonMetadata
{
    [JsonProperty("sources")]
    public List<object>? Sources { get; set; }

    [JsonProperty("previousResourceNames")]
    public List<string>? PreviousResourceNames { get; set; }

    [JsonProperty("linkedPeopleResourceNames")]
    public List<string>? LinkedPeopleResourceNames { get; set; }

    [JsonProperty("deleted")]
    public bool Deleted { get; set; }

    [JsonProperty("objectType")]
    public string? ObjectType { get; set; }
}

public class PersonResponse
{
    [JsonProperty("resourceName")]
    public string? ResourceName { get; set; }

    [JsonProperty("etag")]
    public string? Etag { get; set; }

    [JsonProperty("names")]
    public List<PersonNameDto>? Names { get; set; }

    [JsonProperty("emailAddresses")]
    public List<EmailAddressDto>? EmailAddresses { get; set; }

    [JsonProperty("phoneNumbers")]
    public List<PhoneNumberDto>? PhoneNumbers { get; set; }

    [JsonProperty("photos")]
    public List<PhotoDto>? Photos { get; set; }

    [JsonProperty("organizations")]
    public List<OrganizationDto>? Organizations { get; set; }

    [JsonProperty("addresses")]
    public List<AddressDto>? Addresses { get; set; }

    [JsonProperty("urls")]
    public List<UrlDto>? Urls { get; set; }

    [JsonProperty("biographies")]
    public List<BiographyDto>? Biographies { get; set; }

    [JsonProperty("metadata")]
    public PersonMetadata? Metadata { get; set; }
}

public class ListConnectionsRequest
{
    public string ResourceName { get; set; } = "people/me";
    public int PageSize { get; set; } = 100;
    public string? PageToken { get; set; }
    public string PersonFields { get; set; } = "names,emailAddresses,phoneNumbers,photos";
    public string SortOrder { get; set; } = "LAST_MODIFIED_DESCENDING";
    public string? SyncToken { get; set; }
    public bool RequestSyncToken { get; set; } = false;
}

public class ListConnectionsResponse
{
    [JsonProperty("connections")]
    public List<PersonResponse>? Connections { get; set; }

    [JsonProperty("totalPeople")]
    public int TotalPeople { get; set; }

    [JsonProperty("totalItems")]
    public int TotalItems { get; set; }

    [JsonProperty("nextPageToken")]
    public string? NextPageToken { get; set; }

    [JsonProperty("nextSyncToken")]
    public string? NextSyncToken { get; set; }
}

public class SearchContactsRequest
{
    [Required]
    public string Query { get; set; } = string.Empty;
    public int PageSize { get; set; } = 10;
    public string ReadMask { get; set; } = "names,emailAddresses,phoneNumbers,photos";
}

public class SearchContactResult
{
    [JsonProperty("person")]
    public PersonResponse? Person { get; set; }
}

public class SearchContactsResponse
{
    [JsonProperty("results")]
    public List<SearchContactResult>? Results { get; set; }
}

public class CreateContactRequest
{
    [Required]
    public string GivenName { get; set; } = string.Empty;
    public string? FamilyName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Organization { get; set; }
    public string? JobTitle { get; set; }
    public string? Notes { get; set; }
    public string Type { get; set; } = "personal";
}

public class UpdateContactRequest
{
    [Required]
    public string ResourceName { get; set; } = string.Empty;
    public string? GivenName { get; set; }
    public string? FamilyName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    [Required]
    public string UpdatePersonFields { get; set; } = "names,emailAddresses,phoneNumbers";
}

public class DeleteContactRequest
{
    [Required]
    public string ResourceName { get; set; } = string.Empty;
}

public class BatchGetContactsRequest
{
    [Required]
    public List<string> ResourceNames { get; set; } = new();
    public string PersonFields { get; set; } = "names,emailAddresses,phoneNumbers,photos";
}

public class ContactGroupMetadata
{
    [JsonProperty("updateTime")]
    public string? UpdateTime { get; set; }

    [JsonProperty("deleted")]
    public bool Deleted { get; set; }

    [JsonProperty("sources")]
    public List<object>? Sources { get; set; }
}

public class ContactGroupResponse
{
    [JsonProperty("resourceName")]
    public string? ResourceName { get; set; }

    [JsonProperty("etag")]
    public string? Etag { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("formattedName")]
    public string? FormattedName { get; set; }

    [JsonProperty("groupType")]
    public string? GroupType { get; set; }

    [JsonProperty("memberCount")]
    public int MemberCount { get; set; }

    [JsonProperty("metadata")]
    public ContactGroupMetadata? Metadata { get; set; }
}

public class ListContactGroupsRequest
{
    public int PageSize { get; set; } = 100;
    public string? PageToken { get; set; }
    public string? SyncToken { get; set; }
    public string? GroupFields { get; set; }
}

public class ListContactGroupsResponse
{
    [JsonProperty("contactGroups")]
    public List<ContactGroupResponse>? ContactGroups { get; set; }

    [JsonProperty("totalItems")]
    public int TotalItems { get; set; }

    [JsonProperty("nextPageToken")]
    public string? NextPageToken { get; set; }

    [JsonProperty("nextSyncToken")]
    public string? NextSyncToken { get; set; }
}

public class CreateContactGroupRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
}

public class GetMeRequest
{
    public string PersonFields { get; set; } = "names,emailAddresses,phoneNumbers,photos,organizations,addresses,urls,biographies";
}
