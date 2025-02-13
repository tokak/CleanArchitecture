namespace CleanArchitecture.WepApi.Middleware;

public static class MiddleWareExtension
{
    public static IApplicationBuilder UseErrorMiddlewareExtansions(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}
