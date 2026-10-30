using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.DTOs.TagManager.Common;

public class TagManagerApiResponse
{
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public JToken? Body { get; set; }
}
