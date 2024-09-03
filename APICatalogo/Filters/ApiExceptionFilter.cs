using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiLoggingFilter> _logger;

    public ApiExceptionFilter(ILogger<ApiLoggingFilter> logger)
    {
        _logger = logger;
    } 

    public void OnException(ExceptionContext context)
    {
        context.Result = new ObjectResult("Ocorreu um problema")
        {
            StatusCode = StatusCodes.Status500InternalServerError,
        };
    }
}
