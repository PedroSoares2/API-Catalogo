using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters;

public class ApiLoggingFilter : IActionFilter
{
    private readonly ILogger<ApiLoggingFilter> _logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation($"EXECUTANDO: {context.ActionDescriptor}");
        _logger.LogInformation($"{DateTime.Now}");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation($"FINALIZADO: {context.HttpContext.Response.StatusCode}");
        _logger.LogInformation($"{DateTime.Now}");
    }
}
