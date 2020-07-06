using System;
using Acce.Exceptions;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain
{
    [TestClass]
    public class UpdateMethod : ServiceBase_TestBase
    {
        public UpdateMethod() : base() { }

        [TestMethod]
        public void Success_WithCommit() 
        {
            //setup
            A.CallTo(() => mapper.Map<SampleRecord>(A<SampleEntity>.Ignored)).Returns(new SampleRecord());
            A.CallTo(() => repository.Update(A<SampleRecord>.Ignored)).Returns(true);
            A.CallTo(() => unitOfWork.Commit()).DoesNothing();

            //execution
            Action action = () => service.Update(new SampleEntity { Id = 1 });
            
            //assertions
            action.Should().NotThrow<Exception>();
            A.CallTo(() => mapper.Map<SampleRecord>(A<SampleEntity>.Ignored)).MustHaveHappened();
            A.CallTo(() => repository.Update(A<SampleRecord>.Ignored)).MustHaveHappened();
            A.CallTo(() => unitOfWork.Commit()).MustHaveHappened();
        }

        [TestMethod]
        public void Success_WithoutCommit() 
        {
            //setup
            A.CallTo(() => mapper.Map<SampleRecord>(A<SampleEntity>.Ignored)).Returns(new SampleRecord());
            A.CallTo(() => repository.Update(A<SampleRecord>.Ignored)).Returns(true);
            A.CallTo(() => unitOfWork.Commit()).DoesNothing();

            //execution
            Action action = () => service.Update(new SampleEntity { Id = 1 }, false);
            
            //assertions
            action.Should().NotThrow<Exception>();
            A.CallTo(() => mapper.Map<SampleRecord>(A<SampleEntity>.Ignored)).MustHaveHappened();
            A.CallTo(() => repository.Update(A<SampleRecord>.Ignored)).MustHaveHappened();
            A.CallTo(() => unitOfWork.Commit()).MustNotHaveHappened();
        }

        [TestMethod]
        public void Fail_PropertyNotProvidedException() 
        {
            //setup
            A.CallTo(() => mapper.Map<SampleRecord>(A<SampleEntity>.Ignored)).Returns(new SampleRecord());

            //execution
            Action action = () => service.Update(new SampleEntity());
            
            //assertions
            action.Should().Throw<ValidationIssueException>();
        }    

        [TestMethod]
        public void Fail_ItemNotFoundException() 
        {
            //setup
            A.CallTo(() => mapper.Map<SampleRecord>(A<SampleEntity>.Ignored)).Returns(new SampleRecord());
            A.CallTo(() => repository.Update(A<SampleRecord>.Ignored)).Returns(false);

            //execution
            Action action = () => service.Update(new SampleEntity{ Id = 1 });
            
            //assertions
            action.Should().Throw<ItemNotFoundException>();
            A.CallTo(() => mapper.Map<SampleRecord>(A<SampleEntity>.Ignored)).MustHaveHappened();
            A.CallTo(() => repository.Update(A<SampleRecord>.Ignored)).MustHaveHappened();
        }    
    }
}