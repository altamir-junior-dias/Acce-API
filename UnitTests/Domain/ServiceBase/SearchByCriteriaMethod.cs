using System;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain
{
    [TestClass]
    public class SearchByCriteriaMethod : ServiceBase_TestBase
    {
        public SearchByCriteriaMethod() : base() { }

        [TestMethod]
        public void Success() 
        {
            //setup
            A.CallTo(() => repository.SearchByCriteria(A<object>.Ignored)).Returns(new List<SampleRecord>());
            A.CallTo(() => mapper.Map<IEnumerable<SampleEntity>>(A<List<SampleRecord>>.Ignored)).Returns(new List<SampleEntity>());

            //execution
            IEnumerable<SampleEntity> result = null;
            Action action = () => { result = service.SearchByCriteria(new { }); };
            
            //assertions
            action.Should().NotThrow<Exception>();
            result.Should().NotBeNull();
            A.CallTo(() => repository.SearchByCriteria(A<object>.Ignored)).MustHaveHappened();
            A.CallTo(() => mapper.Map<IEnumerable<SampleEntity>>(A<List<SampleRecord>>.Ignored)).MustHaveHappened();
        }
    }
}