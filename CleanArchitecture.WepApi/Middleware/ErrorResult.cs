using Newtonsoft.Json;

namespace CleanArchitecture.WepApi.Middleware;

public sealed class ErrorResult : ErrorStatusCode
{
    public string Message { get; set; }
}

public class ErrorStatusCode
{
    public int StatusCode { get; set; }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public sealed class ValidationErrorDetails
{
    public IEnumerable<string> Errors { get; set; }
    public int StatusCode { get; set; }
}
