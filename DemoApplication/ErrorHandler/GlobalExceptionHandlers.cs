using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace DemoApplication.ErrorHandler
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var errorData = context.Exception.InnerException;

            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, errorData);

            context.Result = new ResponseMessageResult(response);
        }
    }
}