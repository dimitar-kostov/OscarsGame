using Microsoft.VisualStudio.TestTools.UnitTesting;
using OscarsGame.Business;
using OscarsGame.Data.Interfaces;
using OscarsGame.Entities;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass]
    public class CategoryServiceTests
    {
        [TestMethod]
        public void AddNominationInCategory_ShouldCallCategoryRepositoryMockAndMovieRepositoryMockOnce_WhenTheMovieIsNotInTheDB()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();
            var movieRepositoryMock = MockRepository.GenerateMock<IMovieRepository>();

            //Arrange
            Movie movie = new Movie { Id = 1 };
            var movieCredit = new List<string>() { "1" };
            movieRepositoryMock.Expect(dao => dao.HasMovie(1)).Return(false);
            movieRepositoryMock.Expect(dao => dao.AddMovie(movie)).Repeat.Once();
            categoryRepositoryMock.Expect(dao => dao.AddNomination(1, 1, movieCredit)).Repeat.Once();

            var categoryService = new CategoryService(categoryRepositoryMock, movieRepositoryMock);

            //Act
            categoryService.AddMovieInCategory(1, movie, movieCredit);

            //Assert
            movieRepositoryMock.VerifyAllExpectations();
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void AddMovieInCategory_ShouldNotCallCategoryRepositoryMockAndMovieRepositoryMock_WhenTheMovieIsInTheDB()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();
            var movieRepositoryMock = MockRepository.GenerateMock<IMovieRepository>();

            //Arrange
            Movie movie = new Movie { Id = 1 };
            movieRepositoryMock.Expect(dao => dao.HasMovie(1)).Return(true);
            movieRepositoryMock.Expect(dao => dao.AddMovie(movie)).Repeat.Never();
            movieRepositoryMock.Expect(dao => dao.OverrideMovie(movie)).Repeat.Once();
            categoryRepositoryMock.Expect(dao => dao.AddNomination(Arg<int>.Is.Equal(1), Arg<int>.Is.Equal(1), Arg<List<string>>.Matches(x => x.Count == 0))).Repeat.Once();

            var categoryService = new CategoryService(categoryRepositoryMock, movieRepositoryMock);

            //Act
            categoryService.AddMovieInCategory(1, movie, null);

            //Assert
            movieRepositoryMock.VerifyAllExpectations();
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void AddCategory_ShouldCallCategoryRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();

            //Arrange
            var category = new Category { Id = 1 };

            categoryRepositoryMock.Expect(dao => dao.AddCategory(Arg<Category>.Is.Anything)).Repeat.Once(); ;

            var categoryService = new CategoryService(categoryRepositoryMock);

            //Act
            categoryService.AddCategory(category);

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void DeleteCategory_ShouldCallCategoryRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();

            //Arrange
            categoryRepositoryMock.Expect(dao => dao.DeleteCategory(Arg<int>.Is.Anything)).Repeat.Once(); ;

            var categoryService = new CategoryService(categoryRepositoryMock);

            //Act
            categoryService.DeleteCategory(1);

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void EditCategory_ShouldCallCategoryRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();

            //Arrange
            categoryRepositoryMock.Expect(dao => dao.EditCategory(Arg<Category>.Is.Anything)).Repeat.Once(); ;

            var categoryService = new CategoryService(categoryRepositoryMock);

            //Act
            categoryService.EditCategory(new Category { Id=1});

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetAll_ShouldCallCategoryRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();

            //Arrange
            categoryRepositoryMock.Expect(dao => dao.GetAll()).Return(Arg<IEnumerable<Category>>.Is.Anything).Repeat.Once(); ;

            var categoryService = new CategoryService(categoryRepositoryMock);

            //Act
            categoryService.GetAll();

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetCategory_ShouldCallCategoryRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();

            //Arrange
            categoryRepositoryMock.Expect(dao => dao.GetCategory(Arg<int>.Is.Anything)).Return(Arg<Category>.Is.Anything).Repeat.Once(); ;

            var categoryService = new CategoryService(categoryRepositoryMock);

            //Act
            categoryService.GetCategory(1);

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void MarkAsWinner_ShouldCallCategoryRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();

            //Arrange
            categoryRepositoryMock.Expect(dao => dao.MarkAsWinner(Arg<int>.Is.Anything, Arg<int>.Is.Anything)).Repeat.Once(); ;

            var categoryService = new CategoryService(categoryRepositoryMock);

            //Act
            categoryService.MarkAsWinner(1, 1);

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void RemoveMovieFromCategory_ShouldCallCategoryRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();

            //Arrange
            categoryRepositoryMock.Expect(dao => dao.RemoveNominationFromCategory(Arg<int>.Is.Anything, Arg<int>.Is.Anything)).Repeat.Once(); ;

            var categoryService = new CategoryService(categoryRepositoryMock);

            //Act
            categoryService.RemoveNominationFromCategory(1, 1);

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

    }
}
