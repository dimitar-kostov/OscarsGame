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
    public class WatchedMovieServiceTests
    {
        [TestMethod]
        public void AddWatchedEntity_ShouldCallWatchedMovieRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var watchedMovieRepositoryMock = MockRepository.GenerateMock<IWatchedMovieRepository>();
            watchedMovieRepositoryMock.Expect(dao => dao.AddWatchedEntity(Arg<Watched>.Is.Anything)).Return(Arg<Watched>.Is.Anything).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.WatchedMovieRepository).Return(watchedMovieRepositoryMock);

            var watchedMovieService = new WatchedMovieService(unitOfWorkMockMock);

            //Act
            var watched = new Watched { UserId = default, Movies = new List<Movie>() };
            watchedMovieService.AddWatchedEntity(watched);

            //Assert
            watchedMovieRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetAllUsersWatchedAMovie_ShouldCallWatchedMovieRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var watchedMovieRepositoryMock = MockRepository.GenerateMock<IWatchedMovieRepository>();
            watchedMovieRepositoryMock.Expect(dao => dao.GetAllUsersWatchedAMovie()).Return(Arg<IEnumerable<Watched>>.Is.Anything).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.WatchedMovieRepository).Return(watchedMovieRepositoryMock);

            var watchedMovieService = new WatchedMovieService(unitOfWorkMockMock);

            //Act
            watchedMovieService.GetAllUsersWatchedAMovie();

            //Assert
            watchedMovieRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetAllWatchedMovies_ShouldCallWatchedMovieRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var watchedMovieRepositoryMock = MockRepository.GenerateMock<IWatchedMovieRepository>();
            watchedMovieRepositoryMock.Expect(dao => dao.GetAllWatchedMovies(default)).Return(default).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.WatchedMovieRepository).Return(watchedMovieRepositoryMock);

            var watchedMovieService = new WatchedMovieService(unitOfWorkMockMock);

            //Act
            watchedMovieService.GetAllWatchedMovies(default);

            //Assert
            watchedMovieRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetUserWatchedEntity_ShouldCallWatchedMovieRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var watchedMovieRepositoryMock = MockRepository.GenerateMock<IWatchedMovieRepository>();
            watchedMovieRepositoryMock.Expect(dao => dao.GetUserWatchedEntity(default)).Return(default).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.WatchedMovieRepository).Return(watchedMovieRepositoryMock);

            var watchedMovieService = new WatchedMovieService(unitOfWorkMockMock);

            //Act
            watchedMovieService.GetUserWatchedEntity(default);

            //Assert
            watchedMovieRepositoryMock.VerifyAllExpectations();
        }
    }
}
