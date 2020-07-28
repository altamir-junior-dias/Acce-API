using System.Data;
using Acce.Controllers;
using Acce.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AcceAPI.Controllers
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationIssueException)
            {
                var exception = context.Exception as ValidationIssueException;

                context.HttpContext.Response.StatusCode = 400;
                context.Result = new ObjectResult(new ResponseMessage(ResponseType.ValidationIssues, exception.ValidationIssues));
            }
            else if (context.Exception is ItemNotFoundException)
            {
                var exception = context.Exception as ItemNotFoundException;

                context.HttpContext.Response.StatusCode = 404;
                context.Result = new ObjectResult(new ResponseMessage(ResponseType.DataNotFound, exception.ContextName));
            }
            else if (context.Exception is DataException)
            {
                var exception = context.Exception as DataException;

                context.HttpContext.Response.StatusCode = 500;
                context.Result = new ObjectResult(new ResponseMessage(ResponseType.DatabaseException, exception.Message));
            }
            else
            {
                context.HttpContext.Response.StatusCode = 500;
                context.Result = new ObjectResult(new ResponseMessage(ResponseType.DatabaseException, context.Exception.Message));
            }
        }
    }
}