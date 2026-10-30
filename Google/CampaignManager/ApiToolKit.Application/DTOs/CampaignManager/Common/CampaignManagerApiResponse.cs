using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.DTOs.CampaignManager.Common;

public class CampaignManagerApiResponse
{
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public JToken? Body { get; set; }
}
