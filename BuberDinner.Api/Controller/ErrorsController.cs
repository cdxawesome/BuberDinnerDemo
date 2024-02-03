using BuberDinner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controller
{
    public class ErrorsController : ControllerBase
    {
        //[Route("/error")]
        //public IActionResult Error()
        //{
        //    var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        //    var (statusCode, message) = exception switch
        //    {
        //        IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
        //        _ => (StatusCodes.Status500InternalServerError, "未知错误")
        //    };
        //    return Problem(statusCode: statusCode, title: message);
        //}
    }
}
