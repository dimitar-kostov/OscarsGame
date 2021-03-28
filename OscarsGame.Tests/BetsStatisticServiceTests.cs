using Microsoft.VisualStudio.TestTools.UnitTesting;
using OscarsGame.Business;
using OscarsGame.Domain.Models;
using OscarsGame.Domain.Repositories;
using Rhino.Mocks;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class BetsStatisticServiceTests
    {
        [TestMethod]
        public void GetData_ShouldCallViewModelsRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {

            //Arrange
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();

            List<BetsStatistic> betStatisticList = new List<BetsStatistic>();
            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();
            List<Winners> winners = new List<Winners>();

            viewModelsRepositoryMock.Expect(dao => dao.GetBetsData()).Return(betStatisticList).Repeat.Once();
            viewModelsRepositoryMock.Expect(dao => dao.GetWinner()).Return(winners).Repeat.Once();

            var betService = new BetsStatisticService(viewModelsRepositoryMock);

            //Act
            betService.GetData();

            //Assert
            viewModelsRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetData_ShouldReturnListOfAgregatedDataOfBetsByUser_WhenSingleUserMadeABet()
        {
            //Arrange
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();

            List<BetsStatistic> betStatisticList = new List<BetsStatistic>();
            List<Winners> winners = new List<Winners>();
            var betStatisticObject = new BetsStatistic { Id = 1, Email = "User1", CategoryTitle = "TestCategoryTitle", MovieTitle = "TestMovieTitle" };
            betStatisticList.Add(betStatisticObject);

            List<BetMovieObject> expectedList = new List<BetMovieObject>();
            BetMovieObject expectedObj = new BetMovieObject { MovieTitle = "TestMovieTitle", CategoryTitle = "TestCategoryTitle", IsRightGuess = false };
            expectedList.Add(expectedObj);
            viewModelsRepositoryMock.Expect(dao => dao.GetBetsData()).Return(betStatisticList);
            viewModelsRepositoryMock.Expect(dao => dao.GetWinner()).Return(winners);

            var betService = new BetsStatisticService(viewModelsRepositoryMock);

            //Act
            List<BetObject> resault = betService.GetData();

            //Assert
            Assert.AreEqual("User1", resault[0].UserEmail);
            CollectionAssert.ReferenceEquals(expectedList, resault[0].UserBets);

        }


        [TestMethod]
        public void GetWinner_ShouldCallViewModelsRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();

            List<BetsStatistic> betStatisticList = new List<BetsStatistic>();
            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();
            List<Winners> winners = new List<Winners>();

            viewModelsRepositoryMock.Expect(dao => dao.GetBetsData()).Return(betStatisticList).Repeat.Once();
            viewModelsRepositoryMock.Expect(dao => dao.GetWinner()).Return(winners).Repeat.Once();

            var betService = new BetsStatisticService(viewModelsRepositoryMock);

            //Act
            betService.GetWinner();

            //Assert
            viewModelsRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetCategories_ShouldCallViewModelsRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();
            var watchedMovieRepositoryMock = MockRepository.GenerateMock<IWatchedMovieRepository>();

            List<BetsStatistic> betStatisticList = new List<BetsStatistic>();
            //Arrange
            viewModelsRepositoryMock.Expect(dao => dao.GetBetsData()).Return(betStatisticList).Repeat.Once(); ;

            var betService = new BetsStatisticService(viewModelsRepositoryMock);

            //Act
            betService.GetCategories();

            //Assert
            viewModelsRepositoryMock.VerifyAllExpectations();
        }
    }
}
