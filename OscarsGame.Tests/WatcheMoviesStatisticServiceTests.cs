using Microsoft.VisualStudio.TestTools.UnitTesting;
using OscarsGame.Business;
using OscarsGame.Data.Interfaces;
using OscarsGame.Entities;
using OscarsGame.Entities.StatisticsModels;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass]
    public class WatcheMoviesStatisticServiceTests
    {
        [TestMethod]
        public void GetData_ShouldCalledViewModelsRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();
            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();

            //Arrange

            viewModelsRepositoryMock.Expect(dao => dao.GetWatchedMoviesData()).Return(watchedMovies).Repeat.Once(); ;

            var watcheMoviesStatisticService = new WatcheMoviesStatisticService(viewModelsRepositoryMock);

            //Act
            watcheMoviesStatisticService.GetData();

            //Assert
            viewModelsRepositoryMock.VerifyAllExpectations();
        }
     

        [TestMethod]
        public void GetData_ShouldReturnListOfAgregatedDataOfWatchedMoviesByUser_WhenSingleUserWatchedOneMovies()
        {
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();

            //Arrange
            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();
            WatchedMovies entity1 = new WatchedMovies { Id = 1, Email = "Email1", Title = "Title1" };;
            watchedMovies.Add(entity1);

            viewModelsRepositoryMock.Expect(dao => dao.GetWatchedMoviesData()).Return(watchedMovies);
            var expectedTitlesList = new List<string> { "Title1" };

            //Act
            var watcheMoviesStatisticService = new WatcheMoviesStatisticService(viewModelsRepositoryMock);
            var resault = watcheMoviesStatisticService.GetData();

            //Assert
            Assert.AreEqual("Email1", resault[0].UserEmail);
            CollectionAssert.AreEqual(expectedTitlesList, resault[0].MovieTitles);
        }

        [TestMethod]
        public void GetData_ShouldReturnListOfAgregatedDataOfWatchedMoviesByUser_WhenMultiplesUsersWatchedMultiplesMovie()
        {
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();

            //Arrange
            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();
            WatchedMovies entity1 = new WatchedMovies { Id = 1, Email = "User1", Title = "User1Title1" };
            WatchedMovies entity2 = new WatchedMovies { Id = 2, Email = "User1", Title = "User1Title2" };
            WatchedMovies entity3 = new WatchedMovies { Id = 3, Email = "User2", Title = "User2Title1" };
            WatchedMovies entity4 = new WatchedMovies { Id = 4, Email = "User2", Title = "User2Title2" };
            watchedMovies.Add(entity1);
            watchedMovies.Add(entity2);
            watchedMovies.Add(entity3);
            watchedMovies.Add(entity4);

            viewModelsRepositoryMock.Expect(dao => dao.GetWatchedMoviesData()).Return(watchedMovies);

            var firstUserTitlesList = new List<string> { "User1Title1", "User1Title2" };
            var secondUserTitlesList = new List<string> { "User2Title1", "User2Title2" };

            var watcheMoviesStatisticService = new WatcheMoviesStatisticService(viewModelsRepositoryMock);

            //Act
            var resault = watcheMoviesStatisticService.GetData();

            //Assert
            Assert.AreEqual("User1", resault[0].UserEmail);
            CollectionAssert.AreEqual(firstUserTitlesList, resault[0].MovieTitles);
            Assert.AreEqual("User2", resault[1].UserEmail);
            CollectionAssert.AreEqual(secondUserTitlesList, resault[1].MovieTitles);

        }

        [TestMethod]
        public void GetTitles_ShouldCalledViewModelsRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();

            //Arrange
            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();
            viewModelsRepositoryMock.Expect(dao => dao.GetWatchedMoviesData()).Return(watchedMovies).Repeat.Once(); ;

            var watcheMoviesStatisticService = new WatcheMoviesStatisticService(viewModelsRepositoryMock);

            //Act
            watcheMoviesStatisticService.GetTitles();

            //Assert
            viewModelsRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetTitles_ShouldReturnArrayOftitles_WhenTheRepositoryPassWatchedMoviesData()
        {
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();

            //Arrange
            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();
            WatchedMovies entity1 = new WatchedMovies { Id = 1, Email = "Email1", Title = "Title1" };
            WatchedMovies entity2 = new WatchedMovies { Id = 2, Email = "Email2", Title = "Title2" };
            watchedMovies.Add(entity1);
            watchedMovies.Add(entity2);
            string[] expectedArray = new string[] { "Title1", "Title2" };
            viewModelsRepositoryMock.Expect(dao => dao.GetWatchedMoviesData()).Return(watchedMovies); 

            var watcheMoviesStatisticService = new WatcheMoviesStatisticService(viewModelsRepositoryMock);

            //Act
            var resault= watcheMoviesStatisticService.GetTitles();

            //Assert
            Assert.AreEqual(expectedArray[0], resault.ToArray()[0]);
            Assert.AreEqual(expectedArray[1], resault.ToArray()[1]);
        }

    }
}
