using System;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Repositories
{
    [TestClass]
    public class InsertMethod : RepositoryBase_TestBase
    {
        public InsertMethod() : base() { }
        
        [TestMethod]
        public void Success()
        {
            //setup
            A.CallTo(() => connection.Insert<SampleRecord>(A<SampleRecord>.Ignored, transaction)).Returns(1);

            //execution
            long result = 0;
            Action action = () => { result = sampleRepository.Insert(new SampleRecord()); };

            //assertions
            action.Should().NotThrow<Exception>();
            result.Should().Be(1);
        }
    }
}