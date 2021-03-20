using OscarsGame.Business.Interfaces;
using OscarsGame.Data;
using OscarsGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OscarsGame.Data.Interfaces;
using OscarsGame.Business.Enums;

namespace OscarsGame.Business
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void ChangeMovieStatus(string userId, int movieId)
        {
            _movieRepository.ChangeMovieStatus(userId, movieId);
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _movieRepository.GetAllMovies();
        }

        public IEnumerable<Movie> GetAllMoviesByCriteria(string userId, OrderType orderType, FilterType filterType)
        {
            IEnumerable<Movie> allMovies = new List<Movie>();

            switch (orderType)
            {
                case OrderType.ByName:
                    allMovies = GetAllMovies();
                    break;

                case OrderType.ByNominations:
                    allMovies = MoviesByNominations();
                    break;

                case OrderType.ByProxiadPopularity:
                    allMovies = MoviesByProxiadPopularity();
                    break;
            }

            allMovies = FilterMovies(userId, allMovies, filterType);

            return allMovies;
        }

        private IEnumerable<Movie> MoviesByNominations()
        {
            return GetAllMovies()
                .OrderByDescending(x => x.Nominations.Count)
                .ThenByDescending(x => x.UsersWatchedThisMovie.Count)
                .ThenByDescending(x => x.Title);
        }

        private IEnumerable<Movie> MoviesByProxiadPopularity()
        {
            return GetAllMovies()
                .OrderByDescending(x => x.UsersWatchedThisMovie.Count)
                .ThenByDescending(x => x.Nominations.Count)
                .ThenByDescending(x => x.Title);
        }

        private IEnumerable<Movie> FilterMovies(string userId, IEnumerable<Movie> moviesToFilter, FilterType filterType)
        {
            if (filterType == FilterType.Watched)
            {
                moviesToFilter = 
                    moviesToFilter
                    .Where(x => 
                        x.UsersWatchedThisMovie
                        .Select(y => y.UserId)
                        .Contains(userId));
            }

            if (filterType == FilterType.Unwatched)
            {
                moviesToFilter =
                    moviesToFilter
                    .Where(x =>
                        !x.UsersWatchedThisMovie
                        .Select(y => y.UserId)
                        .Contains(userId));
            }

            return moviesToFilter.ToList();
        }

        public Movie GetMovie(int id)
        {
            return _movieRepository.GetMovie(id);
        }

        public bool HasMovie(int id)
        {
            return _movieRepository.HasMovie(id);
        }
    }
}
