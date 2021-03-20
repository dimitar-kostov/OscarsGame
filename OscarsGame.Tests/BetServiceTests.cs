using Microsoft.VisualStudio.TestTools.UnitTesting;
using OscarsGame.Business;
using OscarsGame.Data.Interfaces;
using OscarsGame.Entities;
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
            var betRepositoryMock = MockRepository.GenerateMock<IBetRepository>();

            //Arrange
            betRepositoryMock.Expect(dao => dao.GetAllUserBets(Arg<string>.Is.Anything)).Return(Arg<IEnumerable<Bet>>.Is.Anything).Repeat.Once();

            var betService = new BetService(betRepositoryMock);

            //Act
            betService.GetAllUserBets("1");

            //Assert
            betRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void MakeBetEntity_Should_BeCalledOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var betRepositoryMock = MockRepository.GenerateMock<IBetRepository>();

            //Arrange
            betRepositoryMock.Expect(dao => dao.MakeBetEntity(Arg<string>.Is.Anything, Arg<int>.Is.Anything)).Repeat.Once();

            var betService = new BetService(betRepositoryMock);

            //Act
            betService.MakeBetEntity("1", 1);

            //Assert
            betRepositoryMock.VerifyAllExpectations();
        }

    }
}
