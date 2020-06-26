using System;
using System.Data;
using Bootstrapping.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Controllers
{
    [TestClass]
    public class ExecuteProcessMethod : BaseController_TestBase
    {
        public ExecuteProcessMethod() : base() {
        }

        [TestMethod]
        public void SuccessFunction() {
            //setup
            Func<long> process = () => { return 0; };
            
            //execution
            var actionResult = controller.ExecuteProcess(process, "");

            //assertion
            actionResult.Should().BeOfType(typeof(OkObjectResult));
        }

        [TestMethod]
        public void SuccessAction() {
            //setup
            Action process = () => { return; };
            
            //execution
            var actionResult = controller.ExecuteProcess(process, "");

            //assertion
            actionResult.Should().BeOfType(typeof(OkResult));
        }

        [TestMethod]
        public void BadRequestFunction_PropertyNotProvidedException() {
            //setup
            Func<long> process = () => { throw new PropertyNotProvidedException("property"); };
            
            //execution
            var actionResult = controller.ExecuteProcess(process, "");

            //assertion
            actionResult.Should().BeOfType(typeof(BadRequestObjectResult));
        }  

        [TestMethod]
        public void BadRequestAction_PropertyNotProvidedException() {
            //setup
            Action process = () => { throw new PropertyNotProvidedException("property"); };
            
            //execution
            var actionResult = controller.ExecuteProcess(process, "");

            //assertion
            actionResult.Should().BeOfType(typeof(BadRequestObjectResult));
        }  

        [DataTestMethod]
        [DataRow("context")]
        [DataRow(null)]
        public void BadRequestFunction_ItemNotFoundException(string context) {
            //setup
            Func<long> process = () => { throw new ItemNotFoundException(context); };
            
            //execution
            var actionResult = controller.ExecuteProcess(process, "");

            //assertion
            actionResult.Should().BeOfType(typeof(BadRequestObjectResult));
        }    

        [DataTestMethod]
        [DataRow("context")]
        [DataRow(null)]
        public void BadRequestAction_ItemNotFoundException(string context) {
            //setup
            Action process = () => { throw new ItemNotFoundException(context); };
            
            //execution
            var actionResult = controller.ExecuteProcess(process, "");

            //assertion
            actionResult.Should().BeOfType(typeof(BadRequestObjectResult));
        }    

        [TestMethod]
        public void BadRequestFunction_DataException() {
            //setup
            Func<long> process = () => { throw new DataException("data"); };
            
            //execution
            var actionResult = controller.ExecuteProcess(process, "");

            //assertion
            actionResult.Should().BeOfType(typeof(BadRequestObjectResult));
        }    

        [TestMethod]
        public void BadRequestAction_DataException() {
            //setup
            Action process = () => { throw new DataException("data"); };
            
            //execution
            var actionResult = controller.ExecuteProcess(process, "");

            //assertion
            actionResult.Should().BeOfType(typeof(BadRequestObjectResult));
        }    

        [TestMethod]
        public void BadRequestFunction_Exception() {
            //setup
            Func<long> process = () => { throw new Exception("error"); };
            
            //execution
            var actionResult = controller.ExecuteProcess(process, "");

            //assertion
            actionResult.Should().BeOfType(typeof(BadRequestObjectResult));
        }    

        [TestMethod]
        public void BadRequestAction_Exception() {
            //setup
            Action process = () => { throw new Exception("error"); };
            
            //execution
            var actionResult = controller.ExecuteProcess(process, "");

            //assertion
            actionResult.Should().BeOfType(typeof(BadRequestObjectResult));
        }    
    }
}