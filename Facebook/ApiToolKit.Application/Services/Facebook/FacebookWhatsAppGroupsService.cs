using ApiToolKit.Application.DTOs.Facebook.WhatsAppPlatform;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using System.Net.Http.Headers;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookWhatsAppGroupsService : WhatsAppServiceBase, IFacebookWhatsAppGroupsService
{
    public FacebookWhatsAppGroupsService(IWhatsAppApiClient api, IFacebookWhatsAppRepository repo) : base(api, repo) { }

    private const string DefaultGroupFields = "subject,description,join_approval_mode,suspended,creation_timestamp,participants,total_participant_count";

    public Task<Result<object>> CreateGroupAsync(WaCreateGroupRequest r)
    {
        var body = new Dictionary<string, object?> { ["messaging_product"] = "whatsapp", ["subject"] = r.Subject };
        if (!string.IsNullOrEmpty(r.Description)) body["description"] = r.Description;
        if (!string.IsNullOrEmpty(r.JoinApprovalMode)) body["join_approval_mode"] = r.JoinApprovalMode;
        return RunAsync("CreateGroup", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/groups", r.AccessToken, body), r);
    }

    public Task<Result<object>> GetGroupsAsync(string phoneNumberId, string accessToken, int? limit, string? after, string? before) =>
        RunAsync("GetGroups", () => Api.GetAsync<object>($"/{phoneNumberId}/groups", accessToken, WhatsAppQuery.Paging(limit, after, before)));

    public Task<Result<object>> GetGroupAsync(string groupId, string accessToken, string? fields) =>
        RunAsync("GetGroup", () => Api.GetAsync<object>($"/{groupId}", accessToken,
            new Dictionary<string, string> { ["fields"] = string.IsNullOrWhiteSpace(fields) ? DefaultGroupFields : fields }));

    public Task<Result<object>> UpdateGroupAsync(string groupId, WaUpdateGroupRequest r)
    {
        if (string.IsNullOrEmpty(r.ProfilePictureBase64))
        {
            var body = new Dictionary<string, object?> { ["messaging_product"] = "whatsapp" };
            if (r.Subject != null) body["subject"] = r.Subject;
            if (r.Description != null) body["description"] = r.Description;
            return RunAsync("UpdateGroup", () => Api.PostAsync<object>($"/{groupId}", r.AccessToken, body), new { groupId, r.Subject, r.Description });
        }

        // A profile picture has to be sent as multipart/form-data.
        return RunAsync("UpdateGroup", () =>
        {
            var form = new MultipartFormDataContent { { new StringContent("whatsapp"), "messaging_product" } };
            if (r.Subject != null) form.Add(new StringContent(r.Subject), "subject");
            if (r.Description != null) form.Add(new StringContent(r.Description), "description");
            var picture = new ByteArrayContent(Convert.FromBase64String(r.ProfilePictureBase64));
            picture.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            form.Add(picture, "profile_picture_file", "profile.jpg");
            return Api.PostMultipartAsync<object>($"/{groupId}", r.AccessToken, form);
        }, new { groupId, r.Subject, r.Description, ProfilePicture = "(binary)" });
    }

    public Task<Result<object>> DeleteGroupAsync(string groupId, string accessToken) =>
        RunAsync("DeleteGroup", () => Api.DeleteAsync<object>($"/{groupId}", accessToken), new { groupId });

    public Task<Result<object>> GetInviteLinkAsync(string groupId, string accessToken) =>
        RunAsync("GetGroupInviteLink", () => Api.GetAsync<object>($"/{groupId}/invite_link", accessToken));

    public Task<Result<object>> ResetInviteLinkAsync(string groupId, string accessToken) =>
        RunAsync("ResetGroupInviteLink", () => Api.PostAsync<object>($"/{groupId}/invite_link", accessToken,
            new { messaging_product = "whatsapp" }), new { groupId });

    public Task<Result<object>> RemoveParticipantsAsync(string groupId, WaGroupParticipantsRequest r)
    {
        if (r.Participants.Count is 0 or > 8)
            return Task.FromResult(Result<object>.Fail("Provide between 1 and 8 participants."));
        return RunAsync("RemoveGroupParticipants", () => Api.DeleteAsync<object>($"/{groupId}/participants", r.AccessToken, body: new
        {
            messaging_product = "whatsapp",
            participants = r.Participants.Select(p => new { user = p })
        }), new { groupId, r.Participants });
    }

    public Task<Result<object>> GetJoinRequestsAsync(string groupId, string accessToken) =>
        RunAsync("GetGroupJoinRequests", () => Api.GetAsync<object>($"/{groupId}/join_requests", accessToken));

    public Task<Result<object>> ApproveJoinRequestsAsync(string groupId, WaJoinRequestsRequest r) =>
        RunAsync("ApproveGroupJoinRequests", () => Api.PostAsync<object>($"/{groupId}/join_requests", r.AccessToken,
            new { messaging_product = "whatsapp", join_requests = r.JoinRequests }), new { groupId, r.JoinRequests });

    public Task<Result<object>> RejectJoinRequestsAsync(string groupId, WaJoinRequestsRequest r) =>
        RunAsync("RejectGroupJoinRequests", () => Api.DeleteAsync<object>($"/{groupId}/join_requests", r.AccessToken,
            body: new { messaging_product = "whatsapp", join_requests = r.JoinRequests }), new { groupId, r.JoinRequests });

    public Task<Result<object>> SendGroupMessageAsync(string groupId, WaGroupMessageRequest r)
    {
        var body = MessageBody(groupId, null, r.Type, "group");
        body[r.Type] = r.Content;
        return RunAsync("SendGroupMessage", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/messages", r.AccessToken, body), r);
    }

    // The group_id parameter is replaced with the group's invite link when delivered.
    public Task<Result<object>> SendGroupInviteTemplateAsync(WaGroupInviteTemplateRequest r)
    {
        var body = MessageBody(r.To, null, "template");
        body["template"] = new
        {
            name = r.TemplateName,
            language = new { code = r.LanguageCode },
            components = new[]
            {
                new { type = "body", parameters = new[] { new { type = "group_id", group_id = r.GroupId } } }
            }
        };
        return RunAsync("SendGroupInviteTemplate", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/messages", r.AccessToken, body), r);
    }
}
