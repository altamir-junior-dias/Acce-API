using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Bootstrapping.Exceptions;

namespace Bootstrapping.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private ActionResult InternalExecuteProcess<T>(Func<T> process, string parameter)
        {
            try
            {
                return Ok(process());
            }
            catch (PropertyNotProvidedException e)
            {
                return BadRequest(new ResponseMessage(ResponseType.PropertyNotProvided, e.PropertyName));
            }
            catch (ItemNotFoundException e)
            {
                return BadRequest(new ResponseMessage(ResponseType.DataNotFound, parameter + (e.ContextName != null ? "." + e.ContextName : "")));
            }
            catch (DataException e)
            {
                return BadRequest(new ResponseMessage(ResponseType.DatabaseException, e.Message));
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseMessage(ResponseType.Exception, e.Message));
            }
        }

        private ActionResult InternalExecuteProcess(Action process, string parameter)
        {
            try
            {
                process();
                return Ok();
            }
            catch (PropertyNotProvidedException e)
            {
                return BadRequest(new ResponseMessage(ResponseType.PropertyNotProvided, e.PropertyName));
            }
            catch (ItemNotFoundException e)
            {
                return BadRequest(new ResponseMessage(ResponseType.DataNotFound, parameter + (e.ContextName != null ? "." + e.ContextName : "")));
            }
            catch (DataException e)
            {
                return BadRequest(new ResponseMessage(ResponseType.DatabaseException, e.Message));
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseMessage(ResponseType.Exception, e.Message));
            }
        }        

        protected ActionResult ExecuteProcess<T>(Func<T> process, string dataName)
        {
            return InternalExecuteProcess<T>(process, dataName);
        }
 
        protected ActionResult ExecuteProcess(Action process, string dataName)
        {
            return InternalExecuteProcess(process, dataName);
        }
    }
}