using OscarsGame.Business.Interfaces;
using OscarsGame.Entities;
using System.Collections.Generic;
using OscarsGame.Data.Interfaces;

namespace OscarsGame.Business
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMovieRepository _movieRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public CategoryService(ICategoryRepository categoryRepository, IMovieRepository movieRepository)
        {
            _categoryRepository = categoryRepository;
            _movieRepository = movieRepository;
        }

        public void AddCategory(Category category)
        {
            _categoryRepository.AddCategory(category);
        }         

        public void AddMovieInCategory(int categoryId, Movie movie, List<string> creditIds)
        {            
            var hasMovie = _movieRepository.HasMovie(movie.Id);
            if (!hasMovie)
            {
                _movieRepository.AddMovie(movie);
            }
            else
            {
                _movieRepository.OverrideMovie(movie);
            }

            _categoryRepository.AddNomination(categoryId, movie.Id, creditIds ?? new List<string>());
        }      
      
        public void DeleteCategory(int id)
        {
            _categoryRepository.DeleteCategory(id);
        }    

        public void EditCategory(Category category)
        {
            _categoryRepository.EditCategory(category);
        }    

        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }                                    
        
        public Category GetCategory(int id)
        {
            return _categoryRepository.GetCategory(id);
        }

        public void MarkAsWinner(int categoryId, int nominationId)
        {
            _categoryRepository.MarkAsWinner(categoryId, nominationId);
        }

        public void RemoveNominationFromCategory(int categoryId, int nominationId)
        {
            _categoryRepository.RemoveNominationFromCategory(categoryId, nominationId);
        }                                

    }
}
