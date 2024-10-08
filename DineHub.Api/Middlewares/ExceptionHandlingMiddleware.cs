using DineHub.Domain.Exceptions;

namespace DineHub.Api.Middlewares;

public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex.Message);

            context.Response.StatusCode = 404;

            await context.Response.WriteAsync(ex.Message);
        }
        catch (UnauthorizedAccessException exception)
        {
            logger.LogWarning(exception.Message);

            context.Response.StatusCode = 401;

            await context.Response.WriteAsync(exception.Message);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, ex.Message);

            context.Response.StatusCode = 500;

            await context.Response.WriteAsync("Something went wrong, try again later");
        }
    }
}