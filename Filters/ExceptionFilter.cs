using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace IngressosAPI.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var result = new ObjectResult(new
            {
                Message = "Erro ao processar a sua requisição",
                Error = context.Exception.Message
            })
            { StatusCode = 500 };
            context.Result = result;
        }
    }
}
