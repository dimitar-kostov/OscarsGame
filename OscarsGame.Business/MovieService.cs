using OscarsGame.Business.Enums;
using OscarsGame.Business.Interfaces;
using OscarsGame.Domain;
using OscarsGame.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OscarsGame.Business
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void ChangeMovieStatus(string userId, int movieId)
        {
            _unitOfWork.MovieRepository.ChangeMovieStatus(userId, movieId);
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _unitOfWork.MovieRepository.GetAllMovies();
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
            return _unitOfWork.MovieRepository.GetMovie(id);
        }

        public bool HasMovie(int id)
        {
            return _unitOfWork.MovieRepository.HasMovie(id);
        }
    }
}
