using System;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Repositories
{
    [TestClass]
    public class DeleteMethod : RepositoryBase_TestBase
    {
        public DeleteMethod() : base() { }
        
        [TestMethod]
        public void Success()
        {
            //setup
            A.CallTo(() => connection.Delete<SampleRecord>(A<SampleRecord>.Ignored, transaction)).Returns(true);

            //execution
            var result = false;
            Action action = () => { result = sampleRepository.Delete(1); };

            //assertions
            action.Should().NotThrow<Exception>();
            result.Should().Be(true);
        }

        [TestMethod]
        public void Fail()
        {
            //setup
            A.CallTo(() => connection.Delete<SampleRecord>(A<SampleRecord>.Ignored, transaction)).Returns(false);

            //execution
            var result = false;
            Action action = () => { result = sampleRepository.Delete(1); };

            //assertions
            action.Should().NotThrow<Exception>();
            result.Should().Be(false);
        }
    }
}