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
    public class CategoryServiceTests
    {
        [TestMethod]
        public void AddNominationInCategory_ShouldCallCategoryRepositoryMockAndMovieRepositoryMockOnce_WhenTheMovieIsNotInTheDB()
        {
            //Arrange
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();
            var movieRepositoryMock = MockRepository.GenerateMock<IMovieRepository>();

            Movie movie = new Movie { Id = 1 };
            var movieCredit = new List<string>() { "1" };
            movieRepositoryMock.Expect(dao => dao.HasMovie(1)).Return(false);
            movieRepositoryMock.Expect(dao => dao.AddMovie(movie)).Repeat.Once();
            categoryRepositoryMock.Expect(dao => dao.AddNomination(1, 1, movieCredit)).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.CategoryRepository).Return(categoryRepositoryMock);
            unitOfWorkMockMock.Stub(uow => uow.MovieRepository).Return(movieRepositoryMock);

            var categoryService = new CategoryService(unitOfWorkMockMock);

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

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.CategoryRepository).Return(categoryRepositoryMock);
            unitOfWorkMockMock.Stub(uow => uow.MovieRepository).Return(movieRepositoryMock);

            var categoryService = new CategoryService(unitOfWorkMockMock);

            //Act
            categoryService.AddMovieInCategory(1, movie, null);

            //Assert
            movieRepositoryMock.VerifyAllExpectations();
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void AddCategory_ShouldCallCategoryRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();
            categoryRepositoryMock.Expect(dao => dao.AddCategory(Arg<Category>.Is.Anything)).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.CategoryRepository).Return(categoryRepositoryMock);

            var categoryService = new CategoryService(unitOfWorkMockMock);

            //Act
            var category = new Category { Id = 1 };
            categoryService.AddCategory(category);

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void DeleteCategory_ShouldCallCategoryRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();
            categoryRepositoryMock.Expect(dao => dao.DeleteCategory(Arg<int>.Is.Anything)).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.CategoryRepository).Return(categoryRepositoryMock);

            var categoryService = new CategoryService(unitOfWorkMockMock);

            //Act
            categoryService.DeleteCategory(1);

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void EditCategory_ShouldCallCategoryRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();
            categoryRepositoryMock.Expect(dao => dao.EditCategory(Arg<Category>.Is.Anything)).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.CategoryRepository).Return(categoryRepositoryMock);

            var categoryService = new CategoryService(unitOfWorkMockMock);

            //Act
            categoryService.EditCategory(new Category { Id = 1 });

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetAll_ShouldCallCategoryRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();
            categoryRepositoryMock.Expect(dao => dao.GetAll()).Return(Arg<IEnumerable<Category>>.Is.Anything).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.CategoryRepository).Return(categoryRepositoryMock);

            var categoryService = new CategoryService(unitOfWorkMockMock);

            //Act
            categoryService.GetAll();

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetCategory_ShouldCallCategoryRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();
            categoryRepositoryMock.Expect(dao => dao.GetCategory(Arg<int>.Is.Anything)).Return(Arg<Category>.Is.Anything).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.CategoryRepository).Return(categoryRepositoryMock);

            var categoryService = new CategoryService(unitOfWorkMockMock);

            //Act
            categoryService.GetCategory(1);

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void MarkAsWinner_ShouldCallCategoryRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();
            categoryRepositoryMock.Expect(dao => dao.MarkAsWinner(Arg<int>.Is.Anything, Arg<int>.Is.Anything)).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.CategoryRepository).Return(categoryRepositoryMock);

            var categoryService = new CategoryService(unitOfWorkMockMock);

            //Act
            categoryService.MarkAsWinner(1, 1);

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void RemoveMovieFromCategory_ShouldCallCategoryRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();
            categoryRepositoryMock.Expect(dao => dao.RemoveNominationFromCategory(Arg<int>.Is.Anything, Arg<int>.Is.Anything)).Repeat.Once();

            var unitOfWorkMockMock = MockRepository.GenerateStub<IUnitOfWork>();
            unitOfWorkMockMock.Stub(uow => uow.CategoryRepository).Return(categoryRepositoryMock);

            var categoryService = new CategoryService(unitOfWorkMockMock);

            //Act
            categoryService.RemoveNominationFromCategory(1, 1);

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

    }
}
