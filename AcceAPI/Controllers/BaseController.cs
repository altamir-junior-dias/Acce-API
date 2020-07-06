using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Acce.Exceptions;

namespace Acce.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private ActionResult InternalExecuteProcess<T>(Func<T> process)
        {
            try
            {
                return Ok(process());
            }
            catch (ValidationIssueException e)
            {
                return BadRequest(new ResponseMessage(ResponseType.ValidationIssues, e.ValidationIssues));
            }
            catch (ItemNotFoundException e)
            {
                return NotFound(new ResponseMessage(ResponseType.DataNotFound, e.ContextName));
            }
            catch (DataException e)
            {
                return StatusCode(500, new ResponseMessage(ResponseType.DatabaseException, e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResponseMessage(ResponseType.Exception, e.Message));
            }
        }

        private ActionResult InternalExecuteProcess(Action process)
        {
            try
            {
                process();
                return Ok();
            }
            catch (ValidationIssueException e)
            {
                return BadRequest(new ResponseMessage(ResponseType.ValidationIssues, e.ValidationIssues));
            }
            catch (ItemNotFoundException e)
            {
                return NotFound(new ResponseMessage(ResponseType.DataNotFound, e.ContextName));
            }
            catch (DataException e)
            {
                return StatusCode(500, new ResponseMessage(ResponseType.DatabaseException, e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResponseMessage(ResponseType.Exception, e.Message));
            }
        }        

        protected ActionResult ExecuteProcess<T>(Func<T> process)
        {
            return InternalExecuteProcess<T>(process);
        }
 
        protected ActionResult ExecuteProcess(Action process)
        {
            return InternalExecuteProcess(process);
        }
    }
}