using OscarsGame.Business.Interfaces;
using OscarsGame.Domain;
using OscarsGame.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OscarsGame.Business
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddCategory(Category category)
        {
            _unitOfWork.CategoryRepository.AddCategory(category);
        }

        public void AddMovieInCategory(int categoryId, Movie movie, List<string> creditIds)
        {
            var hasMovie = _unitOfWork.MovieRepository.HasMovie(movie.Id);
            if (!hasMovie)
            {
                _unitOfWork.MovieRepository.AddMovie(movie);
            }
            else
            {
                _unitOfWork.MovieRepository.OverrideMovie(movie);
            }

            _unitOfWork.CategoryRepository.AddNomination(categoryId, movie.Id, creditIds ?? new List<string>());
        }

        public void DeleteCategory(int id)
        {
            _unitOfWork.CategoryRepository.DeleteCategory(id);
        }

        public void EditCategory(Category category)
        {
            _unitOfWork.CategoryRepository.EditCategory(category);
        }

        public IEnumerable<Category> GetAll()
        {
            return _unitOfWork.CategoryRepository.GetAll();
        }

        public Category GetCategory(int id)
        {
            return _unitOfWork.CategoryRepository.GetCategory(id);
        }

        public Category GetNextCategoryOrDefault(int currentId)
        {
            var categories = GetAll();
            return categories.SkipWhile(x => x.Id != currentId).Skip(1).FirstOrDefault();
        }

        public Category GetPreviousCategoryOrDefault(int currentId)
        {
            var categories = GetAll();
            return categories.TakeWhile(x => x.Id != currentId).LastOrDefault();
        }

        public void MarkAsWinner(int categoryId, int nominationId)
        {
            _unitOfWork.CategoryRepository.MarkAsWinner(categoryId, nominationId);
        }

        public void RemoveNominationFromCategory(int categoryId, int nominationId)
        {
            _unitOfWork.CategoryRepository.RemoveNominationFromCategory(categoryId, nominationId);
        }
    }
}