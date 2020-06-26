using System;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Repositories
{
    [TestClass]
    public class DisposeMethod : UnitOfWork_TestBase {
        public DisposeMethod() : base() {
        }

        [TestMethod]
        public void Success() {
            //setup
            A.CallTo(() => garbageCollectorWrapper.SuppressFinalize(A<Object>.Ignored)).DoesNothing();
            A.CallTo(() => transaction.Dispose()).DoesNothing();
            A.CallTo(() => connection.Dispose()).DoesNothing();

            //execution
            unitOfWork.Dispose();
            unitOfWork = null;
            
            GC.Collect();

            //assertions
            A.CallTo(() => garbageCollectorWrapper.SuppressFinalize(A<Object>.Ignored)).MustHaveHappened();
            A.CallTo(() => transaction.Dispose()).MustHaveHappened();
            A.CallTo(() => connection.Dispose()).MustHaveHappened();
        }
    }
}