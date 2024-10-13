namespace PaymentMicroservice.WebAPI.Middlewares;

/// <summary>
/// Extension that helps to connect middleware and application
/// </summary>
public static class ExceptionHandlerMiddlewareExtension
{
    /// <summary>
    ///   
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}