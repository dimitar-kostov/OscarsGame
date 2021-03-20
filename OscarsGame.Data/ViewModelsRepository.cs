using OscarsGame.Data.Interfaces;
using OscarsGame.Entities.StatisticsModels;
using System.Collections.Generic;
using System.Linq;

namespace OscarsGame.Data
{
    public class ViewModelsRepository: IViewModelsRepository
    {

        public List<WatchedMovies> GetWatchedMoviesData()
        {
            string query = @"SELECT distinct Movies.Id, Movies.Title, WatchedMovies.Watched_UserId as Email 
FROM Movies LEFT JOIN WatchedMovies on Movies.Id = WatchedMovies.Movie_Id 
Join Nominations on Nominations.Movie_Id = WatchedMovies.Movie_Id";
            using (var ctx = new MovieContext())
            {
                List<WatchedMovies> resaults = ctx.Database.SqlQuery<WatchedMovies>(query).ToList();
                return resaults;
            }
        }

        public List<BetsStatistic> GetBetsData()
        {
            string query = @"SELECT Bets.Id, Bets.UserId as Email, Categories.CategoryTtle as CategoryTitle, 
Movies.Title as MovieTitle, Nominations.IsWinner 
FROM Bets 
INNER JOIN Nominations ON Bets.Nomination_Id = Nominations.Id 
INNER JOIN Movies ON Nominations.Movie_Id = Movies.Id 
INNER JOIN Categories ON Nominations.Category_Id = Categories.Id";
            using (var ctx = new MovieContext())
            {
                List<BetsStatistic> resaults = ctx.Database.SqlQuery<BetsStatistic>(query).ToList();
                return resaults;
            }
        }


        public List<Winners> GetWinner()
        {
            string query = @"SELECT Categories.CategoryTtle as Category, Movies.Title as Winner 
FROM Nominations 
INNER JOIN Movies ON Nominations.Movie_Id = Movies.Id 
INNER JOIN Categories ON Nominations.Category_Id = Categories.Id 
WHERE Nominations.IsWinner = 'True'";
            using (var ctx = new MovieContext())
            {
                List<Winners> resaults = ctx.Database.SqlQuery<Winners>(query).ToList();
                return resaults;
            }
        }
    }
}
