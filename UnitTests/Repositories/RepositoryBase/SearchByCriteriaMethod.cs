using System;
using System.Collections.Generic;
using System.Data;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Repositories
{
    [TestClass]
    public class SearchByCriteriaMethod : RepositoryBase_TestBase
    {
        public SearchByCriteriaMethod() : base() { }

        [TestMethod]
        public void Successs() 
        {
            //setup
            A.CallTo(() => connection.Query<SampleRecord>(A<string>.Ignored, A<object>.Ignored, A<IDbTransaction>._)).Returns(new List<SampleRecord>());

            //execution
            IEnumerable<SampleRecord> result = null;
            Action action = () => { result = sampleRepository.SearchByCriteria(new { Id = 1 }); };

            //assertions
            action.Should().NotThrow<Exception>();
            result.Should().NotBeNull();
        }
    }
}