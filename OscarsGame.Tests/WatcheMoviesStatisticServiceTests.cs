using Microsoft.VisualStudio.TestTools.UnitTesting;
using OscarsGame.Business;
using OscarsGame.Domain;
using OscarsGame.Domain.Models;
using OscarsGame.Domain.Repositories;
using Rhino.Mocks;
using System;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class WatcheMoviesStatisticServiceTests
    {
        [TestMethod]
        public void GetData_ShouldCalledViewModelsRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();

            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();
            viewModelsRepositoryMock.Expect(dao => dao.GetWatchedMoviesData()).Return(watchedMovies).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.ViewModelsRepository).Return(viewModelsRepositoryMock);

            var watcheMoviesStatisticService = new WatcheMoviesStatisticService(unitOfWorkMockMock);

            //Act
            watcheMoviesStatisticService.GetData();

            //Assert
            viewModelsRepositoryMock.VerifyAllExpectations();
        }


        [TestMethod]
        public void GetData_ShouldReturnListOfAgregatedDataOfWatchedMoviesByUser_WhenSingleUserWatchedOneMovies()
        {
            //Arrange
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();

            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();
            WatchedMovies entity1 = new WatchedMovies { MovieId = 1, UserId = Guid.NewGuid(), UserDisplayName = "User1", MovieTitle = "Title1" };
            watchedMovies.Add(entity1);

            viewModelsRepositoryMock.Expect(dao => dao.GetWatchedMoviesData()).Return(watchedMovies);
            var expectedTitlesList = new List<string> { "Title1" };

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.ViewModelsRepository).Return(viewModelsRepositoryMock);

            var watcheMoviesStatisticService = new WatcheMoviesStatisticService(unitOfWorkMockMock);

            //Act
            var resault = watcheMoviesStatisticService.GetData();

            //Assert
            Assert.AreEqual("User1", resault[0].UserDisplayMail);
            CollectionAssert.AreEqual(expectedTitlesList, resault[0].MovieTitles);
        }

        [TestMethod]
        public void GetData_ShouldReturnListOfAgregatedDataOfWatchedMoviesByUser_WhenMultiplesUsersWatchedMultiplesMovie()
        {
            //Arrange
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();

            var userId1 = Guid.NewGuid();
            var userId2 = Guid.NewGuid();

            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();
            WatchedMovies entity1 = new WatchedMovies { MovieId = 1, UserId = userId1, UserDisplayName = "User1", MovieTitle = "User1Title1" };
            WatchedMovies entity2 = new WatchedMovies { MovieId = 2, UserId = userId1, UserDisplayName = "User1", MovieTitle = "User1Title2" };
            WatchedMovies entity3 = new WatchedMovies { MovieId = 3, UserId = userId2, UserDisplayName = "User2", MovieTitle = "User2Title1" };
            WatchedMovies entity4 = new WatchedMovies { MovieId = 4, UserId = userId2, UserDisplayName = "User2", MovieTitle = "User2Title2" };
            watchedMovies.Add(entity1);
            watchedMovies.Add(entity2);
            watchedMovies.Add(entity3);
            watchedMovies.Add(entity4);

            viewModelsRepositoryMock.Expect(dao => dao.GetWatchedMoviesData()).Return(watchedMovies);

            var firstUserTitlesList = new List<string> { "User1Title1", "User1Title2" };
            var secondUserTitlesList = new List<string> { "User2Title1", "User2Title2" };

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.ViewModelsRepository).Return(viewModelsRepositoryMock);

            var watcheMoviesStatisticService = new WatcheMoviesStatisticService(unitOfWorkMockMock);

            //Act
            var resault = watcheMoviesStatisticService.GetData();

            //Assert
            Assert.AreEqual("User1", resault[0].UserDisplayMail);
            CollectionAssert.AreEqual(firstUserTitlesList, resault[0].MovieTitles);
            Assert.AreEqual("User2", resault[1].UserDisplayMail);
            CollectionAssert.AreEqual(secondUserTitlesList, resault[1].MovieTitles);

        }
    }
}
