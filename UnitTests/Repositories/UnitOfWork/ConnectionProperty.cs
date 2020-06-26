using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Repositories
{
    [TestClass]
    public class ConnectionAttribute : UnitOfWork_TestBase {
        public ConnectionAttribute() : base() {
        }

        [TestMethod]
        public void Get_CurrentTransaction()
        {
            //execution
            var result = unitOfWork.Connection;

            //assertion
            result.Should().NotBeNull();
        }
    }
}