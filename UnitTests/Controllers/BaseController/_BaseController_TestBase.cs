using System;
using Bootstrapping.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class BaseController_TestBase
    {
        protected BaseControllerWrapper controller;

        public BaseController_TestBase() 
        {
            controller = new SampleController();
        }
    }

    public class SampleController : BaseControllerWrapper
    {
        
    }

    public class BaseControllerWrapper : BaseController
    {
        new public ActionResult ExecuteProcess<T>(Func<T> process, string dataName)
        {
            return base.ExecuteProcess(process, dataName);
        }
 
        new public ActionResult ExecuteProcess(Action process, string dataName)
        {
            return base.ExecuteProcess(process, dataName);
        }

    }
}