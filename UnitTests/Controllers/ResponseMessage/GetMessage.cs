using Bootstrapping.Controllers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        [DataRow(ResponseType.ContentBodyNotProvided, "content", "content must be provided on body.")]
        [DataRow(ResponseType.ParameterNotProvided, "parameter", "parameter must be provided on parameters.")]
        [DataRow(ResponseType.PropertyNotProvided, "property", "The property property was not provided.")]
        [DataRow(ResponseType.RoleFail, "", "")]
        public void Message_DifferentTypes(ResponseType responseType, string reference, string expectedMessage) {
            //setup
            var responseMessage = new ResponseMessage(responseType, reference);

            //execution
            var message = responseMessage.Message;

            //assertion
            message.Should().Be(expectedMessage);
        }
    }
}