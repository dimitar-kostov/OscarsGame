using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OscarsGame.Entities;
using Rhino.Mocks;
using OscarsGame.Data.Interfaces;
using OscarsGame.Business;

namespace UnitTestProject
{
    [TestClass]
    public class MovieServiceTests
    {
        [TestMethod]
        public void ChangeMovieStatus_ShouldCallMovieRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var movieRepositoryMock = MockRepository.GenerateMock<IMovieRepository>();

            //Arrange
            movieRepositoryMock.Expect(dao => dao.ChangeMovieStatus(Arg<string>.Is.Anything, Arg<int>.Is.Anything)).Repeat.Once(); ;

            var movieService = new MovieService(movieRepositoryMock);

            //Act
            movieService.ChangeMovieStatus("1", 1);

            //Assert           
            movieRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetAllMovies_ShouldCallMovieRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var movieRepositoryMock = MockRepository.GenerateMock<IMovieRepository>();

            //Arrange
            movieRepositoryMock.Expect(dao => dao.GetAllMovies()).Repeat.Once(); ;

            var movieService = new MovieService(movieRepositoryMock);

            //Act
            movieService.GetAllMovies();

            //Assert           
            movieRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetMovie_ShouldCallMovieRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var movieRepositoryMock = MockRepository.GenerateMock<IMovieRepository>();

            //Arrange
            movieRepositoryMock.Expect(dao => dao.GetMovie(Arg<int>.Is.Anything)).Repeat.Once(); ;

            var movieService = new MovieService(movieRepositoryMock);

            //Act
            movieService.GetMovie(1);

            //Assert           
            movieRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetMovie_ShouldReturnExpectedMovie_WhenTheCorrectRepositoryIsPassed()
        {
            var movieRepositoryMock = MockRepository.GenerateMock<IMovieRepository>();
            var expectedMovie = new Movie();
            expectedMovie.Id = 1;
            expectedMovie.Title = "Test";
            //Arrange
            movieRepositoryMock.Expect(dao => dao.GetMovie(Arg<int>.Is.Anything)).Return(expectedMovie).Repeat.Once(); ;

            var movieService = new MovieService(movieRepositoryMock);

            //Act
            var returnedMovie= movieService.GetMovie(1);

            //Assert           
            Assert.AreEqual(expectedMovie.Id, returnedMovie.Id);
            
        }
    }
}
