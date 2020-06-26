using System;
using Bootstrapping.Exceptions;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain
{
    [TestClass]
    public class DeleteMethod : ServiceBase_TestBase
    {
        public DeleteMethod() : base() { }

        [TestMethod]
        public void Success_WithCommit() 
        {
            //setup
            A.CallTo(() => repository.Delete(A<long>.Ignored)).Returns(true);
            A.CallTo(() => unitOfWork.Commit()).DoesNothing();

            //execution
            service.Delete(1);
            
            //assertions
            A.CallTo(() => repository.Delete(A<long>.Ignored)).MustHaveHappened();
            A.CallTo(() => unitOfWork.Commit()).MustHaveHappened();
        }

        [TestMethod]
        public void Success_WithoutCommit() 
        {
            //setup
            A.CallTo(() => repository.Delete(A<long>.Ignored)).Returns(true);
            A.CallTo(() => unitOfWork.Commit()).DoesNothing();

            //execution
            service.Delete(1, false);
            
            //assertions
            A.CallTo(() => repository.Delete(A<long>.Ignored)).MustHaveHappened();
            A.CallTo(() => unitOfWork.Commit()).MustNotHaveHappened();
        }

        [TestMethod]
        public void Fail_PropertyNotProvidedException() 
        {
            //setup

            //execution
            Action action = () => service.Delete(0);
            
            //assertions
            action.Should().Throw<PropertyNotProvidedException>();
        }    

        [TestMethod]
        public void Fail_ItemNotFoundException() 
        {
            //setup
            A.CallTo(() => repository.Delete(A<long>.Ignored)).Returns(false);

            //execution
            Action action = () => service.Delete(1);
            
            //assertions
            action.Should().Throw<ItemNotFoundException>();
        }    
    }
}