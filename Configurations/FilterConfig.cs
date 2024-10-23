using IngressosAPI.Filters;
using Microsoft.AspNetCore.Mvc;

public static class FilterConfig
{
    public static void AddCustomFilters(MvcOptions options)
    {
        options.Filters.AddService<AuthorizationFilter>();
        options.Filters.AddService<ActionFilter>();
        options.Filters.AddService<ExceptionFilter>();
    }
}
