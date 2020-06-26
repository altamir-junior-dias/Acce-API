using System;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Repositories
{
    [TestClass]
    public class SearchAllMethod : RepositoryBase_TestBase
    {
        public SearchAllMethod() : base() { }
        
        [TestMethod]
        public void Success()
        {
            //setup
            A.CallTo(() => connection.GetAll<SampleRecord>(transaction)).Returns(new List<SampleRecord>());

            //execution
            IEnumerable<SampleRecord> record = null;
            Action action = () => { record = sampleRepository.SearchAll(); };

            //assertions
            action.Should().NotThrow<Exception>();
            record.Should().NotBeNull();
        }
    }
}