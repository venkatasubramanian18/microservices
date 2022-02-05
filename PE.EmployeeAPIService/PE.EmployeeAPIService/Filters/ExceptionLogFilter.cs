using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.EmployeeAPIService.Models;
using System.IO;
using System.Net;
using Serilog;

namespace PE.EmployeeAPIService.Filters
{
    public class ExceptionLogFilter : IExceptionFilter
    {
        //private readonly ILogger _logger;
        //public ExceptionLogFilter(ILogger<ExceptionLogFilter> logger)
        //{
        //    _logger = logger;
        //}
        /// <summary>
        /// Exception gets thrown when there is an error in application
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            var errorMsg = context.Exception.Message;
            
            //Todo: Problem with my system access to write to command line
            //_logger.LogError(errorMsg);
            context.Result = new ObjectResult(
                new Error()
                {
                    Message = "Error with the Employee Service Api : " + errorMsg,
                    StatusCode = HttpStatusCode.InternalServerError
                });

            Log.Error("StackTrace : " + context.Exception.StackTrace);

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.HttpContext.Response.ContentType = "application/json";
            context.ExceptionHandled = true;
        }
    }
}
