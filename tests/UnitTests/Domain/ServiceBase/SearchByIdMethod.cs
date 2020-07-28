using System;
using Acce.Exceptions;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain
{
    [TestClass]
    public class SearchByIdMethod : ServiceBase_TestBase
    {
        public SearchByIdMethod() : base() { }

        [TestMethod]
        public void Success() 
        {
            //setup
            A.CallTo(() => repository.SearchById(A<long>.Ignored)).Returns(new SampleRecord());
            A.CallTo(() => mapper.Map<SampleEntity>(A<SampleRecord>.Ignored)).Returns(new SampleEntity());

            //execution
            SampleEntity entity = null;
            Action action = () => { entity = service.SearchById(1); };

            //assertion
            action.Should().NotThrow<Exception>();
            entity.Should().NotBeNull();
            A.CallTo(() => repository.SearchById(A<long>.Ignored)).MustHaveHappened();
            A.CallTo(() => mapper.Map<SampleEntity>(A<SampleRecord>.Ignored)).MustHaveHappened();
        }

        [TestMethod]
        public void Fail_ItemNotFoundException() 
        {
            //setup
            A.CallTo(() => repository.SearchById(1)).Returns(null);
            A.CallTo(() => mapper.Map<SampleEntity>(A<SampleRecord>.Ignored)).Returns(null);

            //execution
            Action action = () => { var entity = service.SearchById(1); };

            //assertion
            action.Should().Throw<ItemNotFoundException>();
            A.CallTo(() => mapper.Map<SampleEntity>(A<SampleRecord>.Ignored)).MustHaveHappened();
            A.CallTo(() => repository.SearchById(1)).MustHaveHappened();
        }
    }
}