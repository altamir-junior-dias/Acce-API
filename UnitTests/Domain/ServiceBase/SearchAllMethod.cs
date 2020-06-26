using System;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain
{
    [TestClass]
    public class SearchAllMethod : ServiceBase_TestBase
    {
        public SearchAllMethod() : base() { }

        [TestMethod]
        public void Success() 
        {
            //setup
            A.CallTo(() => repository.SearchAll()).Returns(new List<SampleRecord>());
            A.CallTo(() => mapper.Map<IEnumerable<SampleEntity>>(A<List<SampleRecord>>.Ignored)).Returns(new List<SampleEntity>());

            //execution
            IEnumerable<SampleEntity> result = null;
            Action action = () => { result = service.SearchAll(); };
            
            //assertions
            action.Should().NotThrow<Exception>();
            result.Should().NotBeNull();
            A.CallTo(() => repository.SearchAll()).MustHaveHappened();
            A.CallTo(() => mapper.Map<IEnumerable<SampleEntity>>(A<List<SampleRecord>>.Ignored)).MustHaveHappened();
        }
    }
}