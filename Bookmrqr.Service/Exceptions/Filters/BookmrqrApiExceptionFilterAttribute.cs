namespace Quintor.Bookmrqr.Service.Exceptions.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Quintor.CQRS.Domain.Exceptions;
    using System.Net;

    public class BookmrqrApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is BookmrqrApiException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (context.Exception is ConcurrencyException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            context.Result = new JsonResult(new ApiError(context.Exception.Message));

            base.OnException(context);
        }
    }
}