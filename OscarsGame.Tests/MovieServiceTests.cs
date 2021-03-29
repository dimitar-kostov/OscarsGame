using Microsoft.VisualStudio.TestTools.UnitTesting;
using OscarsGame.Business;
using OscarsGame.Domain;
using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Repositories;
using Rhino.Mocks;

namespace UnitTestProject
{
    [TestClass]
    public class MovieServiceTests
    {
        [TestMethod]
        public void ChangeMovieStatus_ShouldCallMovieRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var movieRepositoryMock = MockRepository.GenerateMock<IMovieRepository>();
            movieRepositoryMock.Expect(dao => dao.ChangeMovieStatus(Arg<string>.Is.Anything, Arg<int>.Is.Anything)).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.MovieRepository).Return(movieRepositoryMock);

            var movieService = new MovieService(unitOfWorkMockMock);

            //Act
            movieService.ChangeMovieStatus("1", 1);

            //Assert           
            movieRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetAllMovies_ShouldCallMovieRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var movieRepositoryMock = MockRepository.GenerateMock<IMovieRepository>();
            movieRepositoryMock.Expect(dao => dao.GetAllMovies()).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.MovieRepository).Return(movieRepositoryMock);

            var movieService = new MovieService(unitOfWorkMockMock);

            //Act
            movieService.GetAllMovies();

            //Assert           
            movieRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetMovie_ShouldCallMovieRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var movieRepositoryMock = MockRepository.GenerateMock<IMovieRepository>();
            movieRepositoryMock.Expect(dao => dao.GetMovie(Arg<int>.Is.Anything)).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.MovieRepository).Return(movieRepositoryMock);

            var movieService = new MovieService(unitOfWorkMockMock);

            //Act
            movieService.GetMovie(1);

            //Assert           
            movieRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetMovie_ShouldReturnExpectedMovie_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var movieRepositoryMock = MockRepository.GenerateMock<IMovieRepository>();
            var expectedMovie = new Movie();
            expectedMovie.Id = 1;
            expectedMovie.Title = "Test";

            movieRepositoryMock.Expect(dao => dao.GetMovie(Arg<int>.Is.Anything)).Return(expectedMovie).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.MovieRepository).Return(movieRepositoryMock);

            var movieService = new MovieService(unitOfWorkMockMock);

            //Act
            var returnedMovie = movieService.GetMovie(1);

            //Assert           
            Assert.AreEqual(expectedMovie.Id, returnedMovie.Id);

        }
    }
}
