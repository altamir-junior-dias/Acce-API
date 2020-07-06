using System;
using System.Data;
using Acce.Exceptions;
using Acce.Domain;
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
            var actionResult = controller.ExecuteProcess(process) as ObjectResult;

            //assertion
            actionResult.Should().BeAssignableTo<OkObjectResult>();
        }

        [TestMethod]
        public void SuccessAction() {
            //setup
            Action process = () => { return; };
            
            //execution
            var actionResult = controller.ExecuteProcess(process) as StatusCodeResult;

            //assertion
            actionResult.Should().BeAssignableTo<OkResult>();
        }

        [TestMethod]
        public void BadRequestFunction_ValidationIssueException() {
            //setup
            Func<long> process = () => { throw new ValidationIssueException(new ValidationIssue()); };
            
            //execution
            var actionResult = controller.ExecuteProcess(process) as ObjectResult;

            //assertion
            actionResult.Should().BeAssignableTo<BadRequestObjectResult>();
        }  

        [TestMethod]
        public void BadRequestAction_ValidationIssueException() {
            //setup
            Action process = () => { throw new ValidationIssueException(new ValidationIssue()); };
            
            //execution
            var actionResult = controller.ExecuteProcess(process) as ObjectResult;

            //assertion
            actionResult.Should().BeAssignableTo<BadRequestObjectResult>();
        }  

        [DataTestMethod]
        [DataRow("context")]
        [DataRow(null)]
        public void BadRequestFunction_ItemNotFoundException(string context) {
            //setup
            Func<long> process = () => { throw new ItemNotFoundException(context); };
            
            //execution
            var actionResult = controller.ExecuteProcess(process) as ObjectResult;

            //assertion
            actionResult.Should().BeAssignableTo<NotFoundObjectResult>();
        }    

        [DataTestMethod]
        [DataRow("context")]
        [DataRow(null)]
        public void BadRequestAction_ItemNotFoundException(string context) {
            //setup
            Action process = () => { throw new ItemNotFoundException(context); };
            
            //execution
            var actionResult = controller.ExecuteProcess(process) as ObjectResult;

            //assertion
            actionResult.Should().BeAssignableTo<NotFoundObjectResult>();
        }    

        [TestMethod]
        public void BadRequestFunction_DataException() {
            //setup
            Func<long> process = () => { throw new DataException("data"); };
            
            //execution
            var actionResult = controller.ExecuteProcess(process) as ObjectResult;

            //assertion
            actionResult.Should().BeAssignableTo<ObjectResult>();
            actionResult.StatusCode.Should().Be(500);
        }    

        [TestMethod]
        public void BadRequestAction_DataException() {
            //setup
            Action process = () => { throw new DataException("data"); };
            
            //execution
            var actionResult = controller.ExecuteProcess(process) as ObjectResult;

            //assertion
            actionResult.Should().BeAssignableTo<ObjectResult>();
            actionResult.StatusCode.Should().Be(500);
        }    

        [TestMethod]
        public void BadRequestFunction_Exception() {
            //setup
            Func<long> process = () => { throw new Exception("error"); };
            
            //execution
            var actionResult = controller.ExecuteProcess(process) as ObjectResult;

            //assertion
            actionResult.Should().BeAssignableTo<ObjectResult>();
            actionResult.StatusCode.Should().Be(500);
        }    

        [TestMethod]
        public void BadRequestAction_Exception() {
            //setup
            Action process = () => { throw new Exception("error"); };
            
            //execution
            var actionResult = controller.ExecuteProcess(process) as ObjectResult;

            //assertion
            actionResult.Should().BeAssignableTo<ObjectResult>();
            actionResult.StatusCode.Should().Be(500);
        }    
    }
}