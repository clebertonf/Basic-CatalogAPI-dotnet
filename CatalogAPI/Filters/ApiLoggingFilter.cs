using Microsoft.AspNetCore.Mvc.Filters;
namespace CatalogAPI.Filters;
public class ApiLoggingFilter : IActionFilter
{
    private readonly ILogger<ApiLoggingFilter> _logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
    {
        _logger = logger;
    }

    // Run before
    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("Executing method 'OnActionExecuted'");
        _logger.LogInformation(DateTime.Now.ToString());
        _logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
    }

    // Run later
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("Executing method 'OnActionExecuting'");
        _logger.LogInformation(DateTime.Now.ToString());
        _logger.LogInformation($"Status code {context.HttpContext.Response.StatusCode}");
    }
}
