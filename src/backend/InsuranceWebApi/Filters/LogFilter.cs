using Microsoft.AspNetCore.Mvc.Filters;

namespace InsuranceWebApi.Filters;

public class LogFilter : IActionFilter
{
    private readonly ILogger _logger;

    public LogFilter(ILogger<LogFilter> logger)
    {
        _logger = logger;
    }

    public async void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context.HttpContext.Request;
        _logger.LogInformation($"Request: {request.Method} {request.Path}");
    }

    public async void OnActionExecuted(ActionExecutedContext context)
    {
        var response = context.HttpContext.Response;
        _logger.LogInformation($"Response: {response.StatusCode}");
    }
}
