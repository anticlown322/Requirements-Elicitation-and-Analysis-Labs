namespace UserMicroservice.WebAPI.Middlewares;

/// <summary>
/// 
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