using Acce.Controllers;
using Acce.Domain;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Controllers
{
    [TestClass]
    public class GetMessage : ResponseMessage_TestBase
    {
        public GetMessage() : base() { }

        [DataTestMethod]
        [DataRow(ResponseType.DatabaseException, "database", "database")]
        [DataRow(ResponseType.DataNotFound, "data", "data not found.")]
        [DataRow(ResponseType.DataNotProvided, "content", "The content data was not provided.")]
        [DataRow(ResponseType.RoleFail, "", "")]
        public void Message_DifferentTypes(ResponseType responseType, string reference, string expectedMessage) 
        {
            //setup
            var responseMessage = new ResponseMessage(responseType, reference);

            //execution
            var message = responseMessage.Message;

            //assertion
            message.Should().Be(expectedMessage);
        }

        [TestMethod]
        public void Message_ValidationIssues()
        {
            //setup
            var validationIssues = new List<ValidationIssue>();
            validationIssues.Add(new ValidationIssue { IssueType = IssueTypeEnum.PropertyNotInformed, PropertyName = "property A" });
            validationIssues.Add(new ValidationIssue { IssueType = IssueTypeEnum.CustomIssue, PropertyName = "property B", Message = "message" });
            
            var responseMessage = new ResponseMessage(ResponseType.ValidationIssues, validationIssues);

            //execution
            var message = responseMessage.Message;

            //assertion
            message.Should().Be("Some validation issues must be resolved: property A must be provided, message");
        }
    }
}