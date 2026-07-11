using ApiToolKit.Application.DTOs.Workspace.Chat;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IChatMemberService
{
    Task<Result<ListMembersResponse>> ListMembersAsync(string userId, ListMembersRequest request);
    Task<Result<MemberResponse>> GetMemberAsync(string userId, string membershipName);
    Task<Result<MemberResponse>> CreateMemberAsync(string userId, CreateMemberRequest request);
    Task<Result<bool>> DeleteMemberAsync(string userId, DeleteMemberRequest request);
}
