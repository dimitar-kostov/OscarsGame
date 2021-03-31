using OscarsGame.Domain.Models;
using OscarsGame.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace OscarsGame.Data
{
    internal class ViewModelsRepository : IViewModelsRepository
    {
        private readonly ApplicationDbContext Context;

        internal ViewModelsRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public List<WatchedMovies> GetWatchedMoviesData()
        {
            string query =
                @"SELECT distinct Movies.Id, Movies.Title, Users.Email 
                FROM Movies 
                        LEFT JOIN WatchedMovies on Movies.Id = WatchedMovies.Movie_Id 
                        Join Nominations on Nominations.Movie_Id = WatchedMovies.Movie_Id
	                    Join Watcheds on Watcheds.Id = WatchedMovies.Watched_Id
	                    Join Users on Users.UserId = Watcheds.UserId";

            return Context.Database.SqlQuery<WatchedMovies>(query).ToList();
        }

        public List<BetsStatistic> GetBetsData()
        {
            string query = @"SELECT Bets.Id, Bets.UserId as Email, Categories.CategoryTtle as CategoryTitle, 
Movies.Title as MovieTitle, Nominations.IsWinner 
FROM Bets 
INNER JOIN Nominations ON Bets.Nomination_Id = Nominations.Id 
INNER JOIN Movies ON Nominations.Movie_Id = Movies.Id 
INNER JOIN Categories ON Nominations.Category_Id = Categories.Id";

            return Context.Database.SqlQuery<BetsStatistic>(query).ToList();
        }


        public List<Winners> GetWinner()
        {
            string query = @"SELECT Categories.CategoryTtle as Category, Movies.Title as Winner 
FROM Nominations 
INNER JOIN Movies ON Nominations.Movie_Id = Movies.Id 
INNER JOIN Categories ON Nominations.Category_Id = Categories.Id 
WHERE Nominations.IsWinner = 'True'";

            return Context.Database.SqlQuery<Winners>(query).ToList();
        }
    }
}
