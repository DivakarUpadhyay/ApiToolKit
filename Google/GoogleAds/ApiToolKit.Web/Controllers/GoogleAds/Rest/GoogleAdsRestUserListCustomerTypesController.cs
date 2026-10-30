using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - UserListCustomerTypes methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestUserListCustomerTypesController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestUserListCustomerTypesController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Attach or remove user list customer types.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateUserListCustomerTypesRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/userListCustomerTypes/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "UserListCustomerTypes.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/userListCustomerTypes:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
