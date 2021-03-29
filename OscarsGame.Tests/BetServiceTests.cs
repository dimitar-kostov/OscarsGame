using Microsoft.VisualStudio.TestTools.UnitTesting;
using OscarsGame.Business;
using OscarsGame.Domain;
using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Repositories;
using Rhino.Mocks;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class BetServiceTests
    {
        [TestMethod]
        public void GetAllUserBets_Should_BeCalledOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var betRepositoryMock = MockRepository.GenerateMock<IBetRepository>();
            betRepositoryMock.Expect(dao => dao.GetAllUserBets(Arg<string>.Is.Anything)).Return(Arg<IEnumerable<Bet>>.Is.Anything).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.BetRepository).Return(betRepositoryMock);

            var betService = new BetService(unitOfWorkMockMock);

            //Act
            betService.GetAllUserBets("1");

            //Assert
            betRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void MakeBetEntity_Should_BeCalledOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var betRepositoryMock = MockRepository.GenerateMock<IBetRepository>();
            betRepositoryMock.Expect(dao => dao.MakeBetEntity(Arg<string>.Is.Anything, Arg<int>.Is.Anything)).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.BetRepository).Return(betRepositoryMock);

            var betService = new BetService(unitOfWorkMockMock);

            //Act
            betService.MakeBetEntity("1", 1);

            //Assert
            betRepositoryMock.VerifyAllExpectations();
        }

    }
}
