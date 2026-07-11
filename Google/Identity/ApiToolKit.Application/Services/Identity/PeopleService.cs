using ApiToolKit.Application.DTOs.Identity.People;
using ApiToolKit.Application.Interfaces.Services.Identity;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Identity;
using ApiToolKit.Domain.Interfaces.Repositories.Identity;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Identity;

public class PeopleService : IPeopleService
{
    private readonly IIdentityApiClient _apiClient;
    private readonly IContactRepository _contactRepo;
    private readonly IContactGroupRepository _groupRepo;
    private const string PeopleBaseUrl = "https://people.googleapis.com/v1/";

    public PeopleService(IIdentityApiClient apiClient, IContactRepository contactRepo, IContactGroupRepository groupRepo)
    {
        _apiClient = apiClient;
        _contactRepo = contactRepo;
        _groupRepo = groupRepo;
    }

    public async Task<Result<PersonResponse>> GetMeAsync(GetMeRequest request, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["personFields"] = request.PersonFields
            };
            var response = await _apiClient.GetAsync<PersonResponse>(
                $"{PeopleBaseUrl}people/me", accessToken, queryParams);
            if (response == null) return Result<PersonResponse>.Fail("No response from People API.");
            return Result<PersonResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<PersonResponse>.Fail($"GetMe failed: {ex.Message}");
        }
    }

    public async Task<Result<ListConnectionsResponse>> ListConnectionsAsync(ListConnectionsRequest request, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["pageSize"] = request.PageSize.ToString(),
                ["personFields"] = request.PersonFields,
                ["sortOrder"] = request.SortOrder,
                ["requestSyncToken"] = request.RequestSyncToken.ToString().ToLower()
            };
            if (!string.IsNullOrEmpty(request.PageToken)) queryParams["pageToken"] = request.PageToken;
            if (!string.IsNullOrEmpty(request.SyncToken)) queryParams["syncToken"] = request.SyncToken;

            var response = await _apiClient.GetAsync<ListConnectionsResponse>(
                $"{PeopleBaseUrl}{request.ResourceName}/connections", accessToken, queryParams);
            if (response == null) return Result<ListConnectionsResponse>.Fail("No response from People API.");
            return Result<ListConnectionsResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<ListConnectionsResponse>.Fail($"ListConnections failed: {ex.Message}");
        }
    }

    public async Task<Result<SearchContactsResponse>> SearchContactsAsync(SearchContactsRequest request, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["query"] = request.Query,
                ["pageSize"] = request.PageSize.ToString(),
                ["readMask"] = request.ReadMask
            };
            var response = await _apiClient.GetAsync<SearchContactsResponse>(
                $"{PeopleBaseUrl}people:searchContacts", accessToken, queryParams);
            if (response == null) return Result<SearchContactsResponse>.Fail("No response from People API.");
            return Result<SearchContactsResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<SearchContactsResponse>.Fail($"SearchContacts failed: {ex.Message}");
        }
    }

    public async Task<Result<PersonResponse>> CreateContactAsync(CreateContactRequest request, string accessToken)
    {
        try
        {
            var body = new
            {
                names = new[] { new { givenName = request.GivenName, familyName = request.FamilyName } },
                emailAddresses = request.Email != null ? new[] { new { value = request.Email, type = request.Type } } : null,
                phoneNumbers = request.Phone != null ? new[] { new { value = request.Phone, type = request.Type } } : null,
                organizations = request.Organization != null ? new[] { new { name = request.Organization, title = request.JobTitle } } : null,
                biographies = request.Notes != null ? new[] { new { value = request.Notes, contentType = "TEXT_PLAIN" } } : null
            };

            var queryParams = new Dictionary<string, string> { ["sources"] = "READ_SOURCE_TYPE_CONTACT" };
            var response = await _apiClient.PostAsync<PersonResponse>(
                $"{PeopleBaseUrl}people:createContact", body, accessToken);
            if (response == null) return Result<PersonResponse>.Fail("No response from People API.");

            if (!string.IsNullOrEmpty(response.ResourceName))
            {
                var contact = new ContactPerson
                {
                    ResourceName = response.ResourceName,
                    GivenName = request.GivenName,
                    FamilyName = request.FamilyName,
                    Email = request.Email,
                    PhoneNumber = request.Phone,
                    Organization = request.Organization,
                    JobTitle = request.JobTitle,
                    Notes = request.Notes,
                    Etag = response.Etag,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await _contactRepo.SaveContactAsync(contact);
            }

            return Result<PersonResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<PersonResponse>.Fail($"CreateContact failed: {ex.Message}");
        }
    }

    public async Task<Result<PersonResponse>> UpdateContactAsync(UpdateContactRequest request, string accessToken)
    {
        try
        {
            var body = new
            {
                resourceName = request.ResourceName,
                names = request.GivenName != null ? new[] { new { givenName = request.GivenName, familyName = request.FamilyName } } : null,
                emailAddresses = request.Email != null ? new[] { new { value = request.Email } } : null,
                phoneNumbers = request.Phone != null ? new[] { new { value = request.Phone } } : null
            };

            var queryParams = new Dictionary<string, string>
            {
                ["updatePersonFields"] = request.UpdatePersonFields
            };
            var url = $"{PeopleBaseUrl}{request.ResourceName}:updateContact?updatePersonFields={Uri.EscapeDataString(request.UpdatePersonFields)}";
            var response = await _apiClient.PatchAsync<PersonResponse>(url, body, accessToken);
            if (response == null) return Result<PersonResponse>.Fail("No response from People API.");

            var contact = new ContactPerson
            {
                ResourceName = request.ResourceName,
                GivenName = request.GivenName,
                FamilyName = request.FamilyName,
                Email = request.Email,
                PhoneNumber = request.Phone,
                Etag = response.Etag,
                UpdatedAt = DateTime.UtcNow
            };
            await _contactRepo.UpdateContactAsync(contact);

            return Result<PersonResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<PersonResponse>.Fail($"UpdateContact failed: {ex.Message}");
        }
    }

    public async Task<Result<bool>> DeleteContactAsync(DeleteContactRequest request, string accessToken)
    {
        try
        {
            var success = await _apiClient.DeleteAsync(
                $"{PeopleBaseUrl}{request.ResourceName}:deleteContact", accessToken);
            if (!success) return Result<bool>.Fail("Failed to delete contact.");
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Fail($"DeleteContact failed: {ex.Message}");
        }
    }

    public async Task<Result<List<PersonResponse>>> BatchGetContactsAsync(BatchGetContactsRequest request, string accessToken)
    {
        try
        {
            var resourceNamesQuery = string.Join("&", request.ResourceNames.Select(r => $"resourceNames={Uri.EscapeDataString(r)}"));
            var url = $"{PeopleBaseUrl}people:batchGet?{resourceNamesQuery}&personFields={Uri.EscapeDataString(request.PersonFields)}";
            var response = await _apiClient.GetAsync<dynamic>(url, accessToken);
            if (response == null) return Result<List<PersonResponse>>.Fail("No response from People API.");

            var responses = JsonConvert.DeserializeObject<List<PersonResponse>>(
                JsonConvert.SerializeObject(response?.responses ?? new List<object>()));
            return Result<List<PersonResponse>>.Ok(responses ?? new List<PersonResponse>());
        }
        catch (Exception ex)
        {
            return Result<List<PersonResponse>>.Fail($"BatchGetContacts failed: {ex.Message}");
        }
    }

    public async Task<Result<ListContactGroupsResponse>> ListContactGroupsAsync(ListContactGroupsRequest request, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["pageSize"] = request.PageSize.ToString()
            };
            if (!string.IsNullOrEmpty(request.PageToken)) queryParams["pageToken"] = request.PageToken;
            if (!string.IsNullOrEmpty(request.SyncToken)) queryParams["syncToken"] = request.SyncToken;
            if (!string.IsNullOrEmpty(request.GroupFields)) queryParams["groupFields"] = request.GroupFields;

            var response = await _apiClient.GetAsync<ListContactGroupsResponse>(
                $"{PeopleBaseUrl}contactGroups", accessToken, queryParams);
            if (response == null) return Result<ListContactGroupsResponse>.Fail("No response from People API.");
            return Result<ListContactGroupsResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<ListContactGroupsResponse>.Fail($"ListContactGroups failed: {ex.Message}");
        }
    }

    public async Task<Result<ContactGroupResponse>> CreateContactGroupAsync(CreateContactGroupRequest request, string accessToken)
    {
        try
        {
            var body = new { contactGroup = new { name = request.Name } };
            var response = await _apiClient.PostAsync<ContactGroupResponse>(
                $"{PeopleBaseUrl}contactGroups", body, accessToken);
            if (response == null) return Result<ContactGroupResponse>.Fail("No response from People API.");

            if (!string.IsNullOrEmpty(response.ResourceName))
            {
                var group = new ContactGroup
                {
                    GroupResourceName = response.ResourceName,
                    Name = request.Name,
                    FormattedName = response.FormattedName,
                    GroupType = response.GroupType,
                    MemberCount = response.MemberCount,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await _groupRepo.SaveGroupAsync(group);
            }

            return Result<ContactGroupResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<ContactGroupResponse>.Fail($"CreateContactGroup failed: {ex.Message}");
        }
    }
}
