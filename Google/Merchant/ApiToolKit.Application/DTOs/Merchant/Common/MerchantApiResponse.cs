using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.DTOs.Merchant.Common;

public class MerchantApiResponse
{
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public JToken? Body { get; set; }
}
