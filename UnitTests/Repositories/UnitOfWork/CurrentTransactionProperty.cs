using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Repositories
{
    [TestClass]
    public class CurrentTransactionAttribute : UnitOfWork_TestBase {
        public CurrentTransactionAttribute() : base() {
        }

        [TestMethod]
        public void Get_CurrentTransaction()
        {
            //execution
            var result = unitOfWork.CurrentTransaction;

            //assertion
            result.Should().NotBeNull();
        }
    }
}