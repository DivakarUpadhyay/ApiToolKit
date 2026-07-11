using ApiToolKit.Application.DTOs.Identity.People;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Identity;

public interface IPeopleService
{
    Task<Result<PersonResponse>> GetMeAsync(GetMeRequest request, string accessToken);
    Task<Result<ListConnectionsResponse>> ListConnectionsAsync(ListConnectionsRequest request, string accessToken);
    Task<Result<SearchContactsResponse>> SearchContactsAsync(SearchContactsRequest request, string accessToken);
    Task<Result<PersonResponse>> CreateContactAsync(CreateContactRequest request, string accessToken);
    Task<Result<PersonResponse>> UpdateContactAsync(UpdateContactRequest request, string accessToken);
    Task<Result<bool>> DeleteContactAsync(DeleteContactRequest request, string accessToken);
    Task<Result<List<PersonResponse>>> BatchGetContactsAsync(BatchGetContactsRequest request, string accessToken);
    Task<Result<ListContactGroupsResponse>> ListContactGroupsAsync(ListContactGroupsRequest request, string accessToken);
    Task<Result<ContactGroupResponse>> CreateContactGroupAsync(CreateContactGroupRequest request, string accessToken);
}
