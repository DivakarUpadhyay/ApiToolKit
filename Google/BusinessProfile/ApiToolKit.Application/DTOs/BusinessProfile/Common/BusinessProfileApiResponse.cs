using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.DTOs.BusinessProfile.Common;

public class BusinessProfileApiResponse
{
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public JToken? Body { get; set; }
}
