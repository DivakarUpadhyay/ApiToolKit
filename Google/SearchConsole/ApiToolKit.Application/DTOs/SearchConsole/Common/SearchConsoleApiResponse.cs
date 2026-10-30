using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.DTOs.SearchConsole.Common;

public class SearchConsoleApiResponse
{
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public JToken? Body { get; set; }
}
