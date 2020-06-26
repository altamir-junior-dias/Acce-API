using System;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Repositories
{
    [TestClass]
    public class UpdateMethod : RepositoryBase_TestBase
    {
        public UpdateMethod() : base() { }
        
        [TestMethod]
        public void Success()
        {
            //setup
            A.CallTo(() => connection.Update<SampleRecord>(A<SampleRecord>.Ignored, transaction)).Returns(true);

            //execution
            var result = false;
            Action action = () => { result = sampleRepository.Update(new SampleRecord()); };

            //assertions
            action.Should().NotThrow<Exception>();
            result.Should().Be(true);
        }

        [TestMethod]
        public void Fail()
        {
            //setup
            A.CallTo(() => connection.Update<SampleRecord>(A<SampleRecord>.Ignored, transaction)).Returns(false);

            //execution
            var result = false;
            Action action = () => { result = sampleRepository.Update(new SampleRecord()); };

            //assertions
            action.Should().NotThrow<Exception>();
            result.Should().Be(false);
        }
    }
}