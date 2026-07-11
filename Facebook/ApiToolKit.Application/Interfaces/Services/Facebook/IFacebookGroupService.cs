using ApiToolKit.Application.DTOs.Facebook.Group;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookGroupService
{
    Task<Result<GroupResponse>> GetGroupAsync(string groupId, string accessToken);
    Task<Result<List<GroupResponse>>> GetGroupsByUserAsync(string userId, string accessToken);
    Task<Result<object>> GetGroupFeedAsync(GroupFeedRequest request, string accessToken);
    Task<Result<List<GroupMemberResponse>>> GetGroupMembersAsync(GroupMembersRequest request, string accessToken);
    Task<Result<bool>> CreateGroupPostAsync(CreateGroupPostRequest request, string accessToken);
    Task<Result<object>> GetGroupAlbumsAsync(GroupAlbumsRequest request, string accessToken);
}
