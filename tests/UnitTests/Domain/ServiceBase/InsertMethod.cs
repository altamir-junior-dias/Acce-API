using System;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain
{
    [TestClass]
    public class InsertMethod : ServiceBase_TestBase
    {
        public InsertMethod() : base() { }

        [TestMethod]
        public void Success_WithCommit() 
        {
            //setup
            A.CallTo(() => mapper.Map<SampleRecord>(A<SampleEntity>.Ignored)).Returns(new SampleRecord());
            A.CallTo(() => repository.Insert(A<SampleRecord>.Ignored)).Returns(1);
            A.CallTo(() => unitOfWork.Commit()).DoesNothing();

            //execution
            long result = 0;
            Action action = () => { result = service.Insert(new SampleEntity()); };
            
            //assertions
            action.Should().NotThrow<Exception>();
            result.Should().Be(1);
            A.CallTo(() => mapper.Map<SampleRecord>(A<SampleEntity>.Ignored)).MustHaveHappened();
            A.CallTo(() => repository.Insert(A<SampleRecord>.Ignored)).MustHaveHappened();
            A.CallTo(() => unitOfWork.Commit()).MustHaveHappened();
        }

        [TestMethod]
        public void Success_WithoutCommit() 
        {
            //setup
            A.CallTo(() => mapper.Map<SampleRecord>(A<SampleEntity>.Ignored)).Returns(new SampleRecord());
            A.CallTo(() => repository.Insert(A<SampleRecord>.Ignored)).Returns(1);
            A.CallTo(() => unitOfWork.Commit()).DoesNothing();

            //execution
            long result = 0;
            Action action = () => { result = service.Insert(new SampleEntity(), false); };
            
            //assertions
            action.Should().NotThrow<Exception>();
            result.Should().Be(1);
            A.CallTo(() => mapper.Map<SampleRecord>(A<SampleEntity>.Ignored)).MustHaveHappened();
            A.CallTo(() => repository.Insert(A<SampleRecord>.Ignored)).MustHaveHappened();
            A.CallTo(() => unitOfWork.Commit()).MustNotHaveHappened();
        }
    }
}