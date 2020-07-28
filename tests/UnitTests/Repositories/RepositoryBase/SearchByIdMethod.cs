using System;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Repositories
{
    [TestClass]
    public class SearchByIdMethod : RepositoryBase_TestBase
    {
        public SearchByIdMethod() : base() { }
        
        [TestMethod]
        public void Success()
        {
            //setup
            A.CallTo(() => connection.Get<SampleRecord>(A<long>.Ignored, transaction)).Returns(new SampleRecord());

            //execution
            SampleRecord record = null;
            Action action = () => { record = sampleRepository.SearchById(1); };

            //assertions
            action.Should().NotThrow<Exception>();
            record.Should().NotBeNull();
        }
    }
}