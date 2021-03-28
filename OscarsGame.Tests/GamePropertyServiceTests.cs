using Microsoft.VisualStudio.TestTools.UnitTesting;
using OscarsGame.Business;
using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Repositories;
using Rhino.Mocks;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class GamePropertyServiceTests
    {
        [TestMethod]
        public void ChangeGameStartDate_ShouldCallGamePropertyRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            //Arrange
            gamePropertyRepositoryMock.Expect(dao => dao.ChangeGameStartDate(Arg<DateTime>.Is.Anything)).Repeat.Once();
            var date = DateTime.Now;

            var gamePropertyService = new GamePropertyService(gamePropertyRepositoryMock);

            //Act
            gamePropertyService.ChangeGameStartDate(date);

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void ChangeGameStopDate_ShouldCallGamePropertyRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            //Arrange
            gamePropertyRepositoryMock.Expect(dao => dao.ChangeGameStopDate(Arg<DateTime>.Is.Anything)).Repeat.Once();
            var date = DateTime.Now;

            var gamePropertyService = new GamePropertyService(gamePropertyRepositoryMock);

            //Act
            gamePropertyService.ChangeGameStopDate(date);

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetGameStartDate_ShouldCallGamePropertyRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            //Arrange
            gamePropertyRepositoryMock.Expect(dao => dao.GetGameStartDate()).Return(Arg<DateTime>.Is.Anything).Repeat.Once();

            var gamePropertyService = new GamePropertyService(gamePropertyRepositoryMock);

            //Act
            gamePropertyService.GetGameStartDate();

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetGameStopDate_ShouldCallGamePropertyRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            //Arrange
            gamePropertyRepositoryMock.Expect(dao => dao.GetGameStopDate()).Return(Arg<DateTime>.Is.Anything).Repeat.Once();

            var gamePropertyService = new GamePropertyService(gamePropertyRepositoryMock);

            //Act
            gamePropertyService.GetGameStopDate();

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void IsGameStopped_ShouldCallGamePropertyRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            //Arrange
            gamePropertyRepositoryMock.Expect(dao => dao.GetDate()).Return(Arg<GameProperties>.Is.Anything).Repeat.Once();

            var gamePropertyService = new GamePropertyService(gamePropertyRepositoryMock);

            //Act
            gamePropertyService.IsGameStopped();

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void IsGameStopped_ShouldReturnTrue_WhenThePassedDateIsInThePast()
        {
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            //Arrange
            var returnedDate = DateTime.MinValue;
            var gamePropertyEntity = new GameProperties();
            gamePropertyEntity.StopGameDate = returnedDate;
            gamePropertyRepositoryMock.Expect(dao => dao.GetDate()).Return(gamePropertyEntity);

            var gamePropertyService = new GamePropertyService(gamePropertyRepositoryMock);

            //Act
            bool recievedValue = gamePropertyService.IsGameStopped();

            //Assert
            Assert.AreEqual(true, recievedValue);

        }

        [TestMethod]
        public void IsGameStopped_ShouldReturnFalse_WhenThePassedDateIsInTheFeuture()
        {
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            //Arrange
            var returnedDate = DateTime.MaxValue;
            var gamePropertyEntity = new GameProperties();
            gamePropertyEntity.StopGameDate = returnedDate;
            gamePropertyRepositoryMock.Expect(dao => dao.GetDate()).Return(gamePropertyEntity);

            var gamePropertyService = new GamePropertyService(gamePropertyRepositoryMock);

            //Act
            bool recievedValue = gamePropertyService.IsGameStopped();

            //Assert
            Assert.AreEqual(false, recievedValue);

        }

        [TestMethod]
        public void IsGameNotStartedYet_ShouldCallGamePropertyRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            //Arrange
            gamePropertyRepositoryMock.Expect(dao => dao.GetDate()).Return(Arg<GameProperties>.Is.Anything).Repeat.Once();

            var gamePropertyService = new GamePropertyService(gamePropertyRepositoryMock);

            //Act
            gamePropertyService.IsGameNotStartedYet();

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void IsGameNotStartedYet_ShouldReturnTrue_WhenThePassedDateIsInTheFeauture()
        {
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            //Arrange
            var returnedDate = DateTime.MaxValue;
            var gamePropertyEntity = new GameProperties();
            gamePropertyEntity.StartGameDate = returnedDate;
            gamePropertyRepositoryMock.Expect(dao => dao.GetDate()).Return(gamePropertyEntity);

            var gamePropertyService = new GamePropertyService(gamePropertyRepositoryMock);

            //Act
            bool recievedValue = gamePropertyService.IsGameNotStartedYet();

            //Assert
            Assert.AreEqual(true, recievedValue);
        }

        [TestMethod]
        public void IsGameNotStartedYet_ShouldReturnFalse_WhenThePassedDateIsInThePast()
        {
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            //Arrange
            var returnedDate = DateTime.MinValue;
            var gamePropertyEntity = new GameProperties();
            gamePropertyEntity.StartGameDate = returnedDate;
            gamePropertyRepositoryMock.Expect(dao => dao.GetDate()).Return(gamePropertyEntity);

            var gamePropertyService = new GamePropertyService(gamePropertyRepositoryMock);

            //Act
            bool recievedValue = gamePropertyService.IsGameNotStartedYet();

            //Assert
            Assert.AreEqual(false, recievedValue);
        }
    }
}
