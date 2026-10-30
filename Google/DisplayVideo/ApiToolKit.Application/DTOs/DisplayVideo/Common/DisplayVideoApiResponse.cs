using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.DTOs.DisplayVideo.Common;

public class DisplayVideoApiResponse
{
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public JToken? Body { get; set; }
}
