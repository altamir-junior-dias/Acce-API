using Bootstrapping.Controllers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Controllers
{
    [TestClass]
    public class GetReference : ResponseMessage_TestBase
    {
        public GetReference() : base() { }

        [DataTestMethod]
        [DataRow(ResponseType.DatabaseException, "")]
        [DataRow(ResponseType.DataNotFound, "data")]
        [DataRow(ResponseType.DataNotProvided, "data")]
        [DataRow(ResponseType.ContentBodyNotProvided, "content")]
        [DataRow(ResponseType.ParameterNotProvided, "parameter")]
        [DataRow(ResponseType.PropertyNotProvided, "property")]
        [DataRow(ResponseType.RoleFail, "")]
        public void GetReference_DifferentType(ResponseType responseType, string expectedReference) 
        {
            //setup
            var responseMessage = new ResponseMessage(responseType, expectedReference);

            //execution
            var reference = responseMessage.Reference;

            //assertion
            reference.Should().Be(expectedReference);
        }
    }
}