using OscarsGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using OscarsGame.Data.Interfaces;

namespace OscarsGame.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        public void AddCategory(Category category)
        {
            using (var ctx = new MovieContext())
            {
                ctx.Caterogries.Add(category);
                ctx.SaveChanges();
            }
        }
        

        public void AddNomination(int categoryId, int movieId, List<string> creditIds)
        {
            using (var ctx = new MovieContext())
            {
                var selectedMovie = ctx.Movies
                    .SingleOrDefault(x => x.Id == movieId);

                var selectedCredits = ctx.Credits
                    .Where(x => creditIds.Contains(x.Id))
                    .ToList();

                var selectedCategory = ctx.Caterogries
                    .SingleOrDefault(cat => cat.Id == categoryId);

                Nomination nomination = new Nomination
                {
                    Category = selectedCategory,
                    Movie = selectedMovie,
                    Credits = selectedCredits,
                };

                ctx.Nominations.Add(nomination);
                ctx.SaveChanges();
            }
        }
              
        public void DeleteCategory(int id)
        {

            using (var ctx = new MovieContext())
            {
                var databaseCategory = ctx.Caterogries.
                    Include(x => x.Nominations).
                    SingleOrDefault(x => x.Id == id);

                ctx.Entry(databaseCategory).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

        public void EditCategory(Category category)
        {
            using (var ctx = new MovieContext())
            {
                ctx.Entry(category).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }
       
        public IEnumerable<Category> GetAll()
        {
            using (var ctx = new MovieContext())
            {
                var databaseCategory = ctx.Caterogries
                    .Include(cat => cat.Nominations.Select(nom => nom.Movie))
                    .Include(cat => cat.Nominations.Select(nom => nom.Credits))
                    .Include(cat => cat.Nominations.Select(bet => bet.Bets))
                    .OrderBy(cat => cat.Id)
                    .ToList();

                return databaseCategory;
            }
        }                                          

        public Category GetCategory(int id)
        {
            using (var ctx = new MovieContext())
            {
                var foundedCategory = ctx.Caterogries
                   .Include(cat => cat.Nominations)
                   .Include(cat => cat.Nominations.Select(nom => nom.Movie))
                   .Include(cat => cat.Nominations.Select(nom => nom.Credits))
                   .Include(cat => cat.Nominations.Select(nom => nom.Bets))
                   .Include(cat => cat.Nominations.Select(nom => nom.Movie.UsersWatchedThisMovie))
                   .Where(cat => cat.Id == id)
                   .SingleOrDefault();
                return foundedCategory;
            }
        }

        public void MarkAsWinner(int categoryId, int nominationId)
        {
            using (var ctx = new MovieContext())
            {
                var selectedCategory = ctx.Caterogries
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

                ctx.SaveChanges();
            }
        }

        public void RemoveNominationFromCategory(int categoryId, int nominationId)
        {
            using (var ctx = new MovieContext())
            {
                var databaseCategory = ctx.Caterogries.Include(cat => cat.Nominations).SingleOrDefault(x => x.Id == categoryId);
                var foundNomination = databaseCategory.Nominations.FirstOrDefault(x => x.Id == nominationId);
                databaseCategory.Nominations.Remove(foundNomination);
                ctx.SaveChanges();
            }
        }                               

    }
}
