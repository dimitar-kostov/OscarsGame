using Microsoft.VisualStudio.TestTools.UnitTesting;
using OscarsGame.Business;
using OscarsGame.Domain;
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
            //Arrange
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();
            gamePropertyRepositoryMock.Expect(dao => dao.ChangeGameStartDate(Arg<DateTime>.Is.Anything)).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.GamePropertyRepository).Return(gamePropertyRepositoryMock);

            var gamePropertyService = new GamePropertyService(unitOfWorkMockMock);

            //Act
            var date = DateTime.Now;
            gamePropertyService.ChangeGameStartDate(date);

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void ChangeGameStopDate_ShouldCallGamePropertyRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            gamePropertyRepositoryMock.Expect(dao => dao.ChangeGameStopDate(Arg<DateTime>.Is.Anything)).Repeat.Once();
            var date = DateTime.Now;

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.GamePropertyRepository).Return(gamePropertyRepositoryMock);

            var gamePropertyService = new GamePropertyService(unitOfWorkMockMock);

            //Act
            gamePropertyService.ChangeGameStopDate(date);

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetGameStartDate_ShouldCallGamePropertyRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            gamePropertyRepositoryMock.Expect(dao => dao.GetGameStartDate()).Return(Arg<DateTime>.Is.Anything).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.GamePropertyRepository).Return(gamePropertyRepositoryMock);

            var gamePropertyService = new GamePropertyService(unitOfWorkMockMock);

            //Act
            gamePropertyService.GetGameStartDate();

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetGameStopDate_ShouldCallGamePropertyRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            gamePropertyRepositoryMock.Expect(dao => dao.GetGameStopDate()).Return(Arg<DateTime>.Is.Anything).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.GamePropertyRepository).Return(gamePropertyRepositoryMock);

            var gamePropertyService = new GamePropertyService(unitOfWorkMockMock);

            //Act
            gamePropertyService.GetGameStopDate();

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void IsGameStopped_ShouldCallGamePropertyRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            gamePropertyRepositoryMock.Expect(dao => dao.GetDate()).Return(Arg<GameProperties>.Is.Anything).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.GamePropertyRepository).Return(gamePropertyRepositoryMock);

            var gamePropertyService = new GamePropertyService(unitOfWorkMockMock);

            //Act
            gamePropertyService.IsGameStopped();

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void IsGameStopped_ShouldReturnTrue_WhenThePassedDateIsInThePast()
        {
            //Arrange
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            var returnedDate = DateTime.MinValue;
            var gamePropertyEntity = new GameProperties();
            gamePropertyEntity.StopGameDate = returnedDate;
            gamePropertyRepositoryMock.Expect(dao => dao.GetDate()).Return(gamePropertyEntity);

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.GamePropertyRepository).Return(gamePropertyRepositoryMock);

            var gamePropertyService = new GamePropertyService(unitOfWorkMockMock);

            //Act
            bool recievedValue = gamePropertyService.IsGameStopped();

            //Assert
            Assert.AreEqual(true, recievedValue);

        }

        [TestMethod]
        public void IsGameStopped_ShouldReturnFalse_WhenThePassedDateIsInTheFeuture()
        {
            //Arrange
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            var returnedDate = DateTime.MaxValue;
            var gamePropertyEntity = new GameProperties();
            gamePropertyEntity.StopGameDate = returnedDate;
            gamePropertyRepositoryMock.Expect(dao => dao.GetDate()).Return(gamePropertyEntity);

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.GamePropertyRepository).Return(gamePropertyRepositoryMock);

            var gamePropertyService = new GamePropertyService(unitOfWorkMockMock);

            //Act
            bool recievedValue = gamePropertyService.IsGameStopped();

            //Assert
            Assert.AreEqual(false, recievedValue);

        }

        [TestMethod]
        public void IsGameNotStartedYet_ShouldCallGamePropertyRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();
            gamePropertyRepositoryMock.Expect(dao => dao.GetDate()).Return(Arg<GameProperties>.Is.Anything).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.GamePropertyRepository).Return(gamePropertyRepositoryMock);

            var gamePropertyService = new GamePropertyService(unitOfWorkMockMock);

            //Act
            gamePropertyService.IsGameNotStartedYet();

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void IsGameNotStartedYet_ShouldReturnTrue_WhenThePassedDateIsInTheFeauture()
        {
            //Arrange
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            var returnedDate = DateTime.MaxValue;
            var gamePropertyEntity = new GameProperties();
            gamePropertyEntity.StartGameDate = returnedDate;
            gamePropertyRepositoryMock.Expect(dao => dao.GetDate()).Return(gamePropertyEntity);

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.GamePropertyRepository).Return(gamePropertyRepositoryMock);

            var gamePropertyService = new GamePropertyService(unitOfWorkMockMock);

            //Act
            bool recievedValue = gamePropertyService.IsGameNotStartedYet();

            //Assert
            Assert.AreEqual(true, recievedValue);
        }

        [TestMethod]
        public void IsGameNotStartedYet_ShouldReturnFalse_WhenThePassedDateIsInThePast()
        {
            //Arrange
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            var returnedDate = DateTime.MinValue;
            var gamePropertyEntity = new GameProperties();
            gamePropertyEntity.StartGameDate = returnedDate;
            gamePropertyRepositoryMock.Expect(dao => dao.GetDate()).Return(gamePropertyEntity);

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.GamePropertyRepository).Return(gamePropertyRepositoryMock);

            var gamePropertyService = new GamePropertyService(unitOfWorkMockMock);

            //Act
            bool recievedValue = gamePropertyService.IsGameNotStartedYet();

            //Assert
            Assert.AreEqual(false, recievedValue);
        }
    }
}
