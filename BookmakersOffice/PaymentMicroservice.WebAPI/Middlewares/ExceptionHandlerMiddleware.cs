using Newtonsoft.Json;
using PaymentMicroservice.Business.Exceptions;
using PaymentMicroservice.WebAPI.Models;

namespace PaymentMicroservice.WebAPI.Middlewares;

/// <summary>
/// Custom middleware to handle various exceptions and log them
/// </summary>
public class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger">Logger instance for exceptions</param>
    /// <exception cref="ArgumentNullException">If null instead of logger was given</exception>
    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Creates, catches and logs exceptions from Http contexts if there are any errors / non-valid data  
    /// </summary>
    /// <param name="context">Http context to process</param>
    /// <param name="next">Function for Http context processing</param>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var message = CreateMessage(context, ex);
            _logger.LogError(message, ex);

            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        var result = new ResultModel() { IsSuccessful = false, Message = e.Message };
        int statusCode;

        if (e is ArgumentException || e is ArgumentNullException)
        {
            statusCode = StatusCodes.Status400BadRequest;
        }
        else if (e is SampleException)
        {
            statusCode = StatusCodes.Status422UnprocessableEntity;
        }
        else
        {
            statusCode = StatusCodes.Status500InternalServerError;
            result.Message = "Unknown error, please contact the system admin";
        }

        _logger.LogError(e, e.Message);

        var response = JsonConvert.SerializeObject(result, Formatting.Indented,
            new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(response);
    }

    private string CreateMessage(HttpContext context, Exception e)
    {
        var message = $"Exception caught in global error handler, exception message: {e.Message}, exception stack: {e.StackTrace}";

        if (e.InnerException != null)
        {
            message = $"{message}, inner exception message {e.InnerException.Message}, inner exception stack {e.InnerException.StackTrace}";
        }

        return $"{message} RequestId: {context.TraceIdentifier}";
    }
}