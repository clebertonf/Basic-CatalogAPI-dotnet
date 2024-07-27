using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace CatalogAPI.Filters;
public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;

    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "An error occurred during your request!");
        context.Result = new ObjectResult("An error occurred during your request!")
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}
