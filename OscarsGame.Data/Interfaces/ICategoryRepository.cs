using OscarsGame.Entities;
using System.Collections.Generic;

namespace OscarsGame.Data.Interfaces
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);

        void AddNomination(int categoryId, int movieId, List<string> creditIds);

        void DeleteCategory(int id);

        void EditCategory(Category category);

        IEnumerable<Category> GetAll();

        Category GetCategory(int id);

        void MarkAsWinner(int categoryId, int nominationId);

        void RemoveNominationFromCategory(int categoryId, int nominationId);

    }
}
