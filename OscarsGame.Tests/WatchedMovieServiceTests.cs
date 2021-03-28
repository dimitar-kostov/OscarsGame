using Microsoft.VisualStudio.TestTools.UnitTesting;
using OscarsGame.Business;
using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Repositories;
using Rhino.Mocks;
using System;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class WatchedMovieServiceTests
    {
        [TestMethod]
        public void AddWatchedEntity_ShouldCallWatchedMovieRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var watchedMovieRepositoryMock = MockRepository.GenerateMock<IWatchedMovieRepository>();
            var watched = new Watched { UserId = "test", Movies = new List<Movie>() };
            //Arrange
            watchedMovieRepositoryMock.Expect(dao => dao.AddWatchedEntity(Arg<Watched>.Is.Anything)).Return(Arg<Watched>.Is.Anything).Repeat.Once();
            var date = DateTime.Now;

            var watchedMovieService = new WatchedMovieService(watchedMovieRepositoryMock);

            //Act
            watchedMovieService.AddWatchedEntity(watched);

            //Assert
            watchedMovieRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetAllUsersWatchedAMovie_ShouldCallWatchedMovieRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var watchedMovieRepositoryMock = MockRepository.GenerateMock<IWatchedMovieRepository>();
            //Arrange
            watchedMovieRepositoryMock.Expect(dao => dao.GetAllUsersWatchedAMovie()).Return(Arg<IEnumerable<Watched>>.Is.Anything).Repeat.Once();
            var date = DateTime.Now;

            var watchedMovieService = new WatchedMovieService(watchedMovieRepositoryMock);

            //Act
            watchedMovieService.GetAllUsersWatchedAMovie();

            //Assert
            watchedMovieRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetAllWatchedMovies_ShouldCallWatchedMovieRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var watchedMovieRepositoryMock = MockRepository.GenerateMock<IWatchedMovieRepository>();
            //Arrange
            watchedMovieRepositoryMock.Expect(dao => dao.GetAllWatchedMovies(Arg<string>.Is.Anything)).Return(Arg<IEnumerable<Watched>>.Is.Anything).Repeat.Once();
            var date = DateTime.Now;

            var watchedMovieService = new WatchedMovieService(watchedMovieRepositoryMock);

            //Act
            watchedMovieService.GetAllWatchedMovies("test");

            //Assert
            watchedMovieRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetUserWatchedEntity_ShouldCallWatchedMovieRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var watchedMovieRepositoryMock = MockRepository.GenerateMock<IWatchedMovieRepository>();
            //Arrange
            watchedMovieRepositoryMock.Expect(dao => dao.GetUserWatchedEntity(Arg<string>.Is.Anything)).Return(Arg<Watched>.Is.Anything).Repeat.Once();
            var date = DateTime.Now;

            var watchedMovieService = new WatchedMovieService(watchedMovieRepositoryMock);

            //Act
            watchedMovieService.GetUserWatchedEntity("test");

            //Assert
            watchedMovieRepositoryMock.VerifyAllExpectations();
        }
    }
}
