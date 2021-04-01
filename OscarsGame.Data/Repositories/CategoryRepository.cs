using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OscarsGame.Data
{
    internal class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        internal CategoryRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public void AddCategory(Category category)
        {
            Context.Caterogries.Add(category);
            Context.SaveChanges();
        }

        public void AddNomination(int categoryId, int movieId, List<string> creditIds)
        {
            var selectedMovie = Context.Movies
                    .SingleOrDefault(x => x.Id == movieId);

            var selectedCredits = Context.Credits
                .Where(x => creditIds.Contains(x.Id))
                .ToList();

            var selectedCategory = Context.Caterogries
                .SingleOrDefault(cat => cat.Id == categoryId);

            Nomination nomination = new Nomination
            {
                Category = selectedCategory,
                Movie = selectedMovie,
                Credits = selectedCredits,
            };

            Context.Nominations.Add(nomination);
            Context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var databaseCategory = Context.Caterogries.
                    Include(x => x.Nominations).
                    SingleOrDefault(x => x.Id == id);

            Context.Entry(databaseCategory).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void EditCategory(Category category)
        {
            Context.Entry(category).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            var databaseCategory = Context.Caterogries
                    .Include(cat => cat.Nominations.Select(nom => nom.Movie))
                    .Include(cat => cat.Nominations.Select(nom => nom.Credits))
                    .Include(cat => cat.Nominations.Select(bet => bet.Bets))
                    .OrderBy(cat => cat.Id)
                    .ToList();

            return databaseCategory;
        }

        public Category GetCategory(int id)
        {
            var foundedCategory = Context.Caterogries
                   .Include(cat => cat.Nominations)
                   .Include(cat => cat.Nominations.Select(nom => nom.Movie))
                   .Include(cat => cat.Nominations.Select(nom => nom.Credits))
                   .Include(cat => cat.Nominations.Select(nom => nom.Bets))
                   .Include(cat => cat.Nominations.Select(nom => nom.Movie.UsersWatchedThisMovie))
                   .Where(cat => cat.Id == id)
                   .SingleOrDefault();

            return foundedCategory;
        }

        public void MarkAsWinner(int categoryId, int nominationId)
        {
            var selectedCategory = Context.Caterogries
                    .Include(cat => cat.Nominations)
                    .Single(x => x.Id == categoryId);

            var winnerNomination = selectedCategory
                .Nominations
                .Where(x => x.IsWinner)
                .SingleOrDefault();

            if (winnerNomination != null)
            {
                winnerNomination.IsWinner = false;
            }

            var selectedNomination = selectedCategory
                .Nominations
                .Single(x => x.Id == nominationId);

            selectedNomination.IsWinner = true;

            Context.SaveChanges();
        }

        public void RemoveNominationFromCategory(int categoryId, int nominationId)
        {
            var databaseCategory = Context.Caterogries.Include(cat => cat.Nominations).SingleOrDefault(x => x.Id == categoryId);
            var foundNomination = databaseCategory.Nominations.FirstOrDefault(x => x.Id == nominationId);
            databaseCategory.Nominations.Remove(foundNomination);
            Context.SaveChanges();
        }
    }
}
