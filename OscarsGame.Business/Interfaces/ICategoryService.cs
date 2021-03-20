using OscarsGame.Entities;
using System.Collections.Generic;

namespace OscarsGame.Business.Interfaces
{
    public interface ICategoryService
    {
        void AddCategory(Category category);

        void AddMovieInCategory(int categoryId, Movie movie, List<string> creditIds);

        void DeleteCategory(int id);

        void EditCategory(Category category);

        IEnumerable<Category> GetAll();

        Category GetCategory(int id);

        void MarkAsWinner(int categoryId, int nominationId);

        void RemoveNominationFromCategory(int categoryId, int nominationId);
    }
}
