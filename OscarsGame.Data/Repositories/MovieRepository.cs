using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OscarsGame.Data
{
    internal class MovieRepository : Repository<Movie>, IMovieRepository
    {
        internal MovieRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public void AddMovie(Movie movie)
        {
            Context.Movies.Add(movie);
            Context.SaveChanges();
        }

        public void OverrideMovie(Movie movie)
        {
            Context.Entry(movie).State = EntityState.Modified;

            foreach (MovieCredit credit in movie.Credits)
            {
                if (Context.Credits.Any(x => x.Id == credit.Id))
                {
                    Context.Entry(credit).State = EntityState.Modified;
                }
                else
                {
                    Context.Entry(credit).State = EntityState.Added;
                }
            }

            Context.SaveChanges();
        }

        public void ChangeMovieStatus(string userId, int movieId)
        {
            var movie = Context.Movies.SingleOrDefault(x => x.Id == movieId);
            var watchedEntity = Context.Watched.Include(w => w.Movies).SingleOrDefault(x => x.UserId == userId);
            if (watchedEntity.Movies.Any(m => m.Id == movieId))
            {
                watchedEntity.Movies.Remove(movie);
            }
            else
            {
                watchedEntity.Movies.Add(movie);
            }

            Context.SaveChanges();
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return Context.Movies
                    .Include(x => x.UsersWatchedThisMovie)
                    .Include(x => x.Nominations.Select(nom => nom.Category))
                    .Where(x => x.Nominations.Any())
                    .OrderBy(x => x.Title)
                    .ToList();
        }

        public Movie GetMovie(int id)
        {
            return Context.Movies
                    .Include(m => m.Credits)
                    .Include(m => m.Nominations.Select(x => x.Category))
                    .Include(m => m.Nominations.Select(x => x.Credits))
                    .Where(m => m.Id == id)
                    .SingleOrDefault();
        }

        public bool HasMovie(int id)
        {
            return Context.Movies.Any(m => m.Id == id);
        }
    }
}
