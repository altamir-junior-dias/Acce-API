using System;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Repositories
{
    [TestClass]
    public class CommitMethod : UnitOfWork_TestBase {
        public CommitMethod() : base() {
        }

        [TestMethod]
        public void Success() {
            //setup
            A.CallTo(() => transaction.Commit()).DoesNothing();
            A.CallTo(() => transaction.Dispose()).DoesNothing();

            //execution
            Action action = () => unitOfWork.Commit();

            //assertions
            action.Should().NotThrow<Exception>();
            A.CallTo(() => transaction.Commit()).MustHaveHappened();
            A.CallTo(() => transaction.Dispose()).MustHaveHappened();
        }
        
        [TestMethod]
        public void Rollback() {
            //setup
            A.CallTo(() => transaction.Commit()).Throws(new Exception());
            A.CallTo(() => transaction.Rollback()).DoesNothing();
            A.CallTo(() => transaction.Dispose()).DoesNothing();

            //execution
            Action action = () => unitOfWork.Commit();

            //assertions
            action.Should().Throw<Exception>();
            A.CallTo(() => transaction.Commit()).MustHaveHappened();
            A.CallTo(() => transaction.Rollback()).MustHaveHappened();
            A.CallTo(() => transaction.Dispose()).MustHaveHappened();
        }
    }
}