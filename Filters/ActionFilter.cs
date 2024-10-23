using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace IngressosAPI.Filters
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            /// Iniciando a medição de tempo de execução
            context.HttpContext.Items["StartTime"] = Stopwatch.StartNew();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var stopwatch = (Stopwatch)context.HttpContext.Items["StartTime"];
            stopwatch.Stop();


            var executionTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Tempo de execução:{executionTime} ms");

        }

    }
}
