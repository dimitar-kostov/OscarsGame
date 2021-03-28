using OscarsGame.Business.Interfaces;
using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Repositories;
using System.Collections.Generic;

namespace OscarsGame.Business
{
    public class WatchedMovieService : IWatchedMovieService
    {

        private readonly IWatchedMovieRepository _watchedMovieRepository;


        public WatchedMovieService(IWatchedMovieRepository watchedMovieRepository)
        {
            _watchedMovieRepository = watchedMovieRepository;
        }

        public Watched AddWatchedEntity(Watched watchedEntity)
        {
            return _watchedMovieRepository.AddWatchedEntity(watchedEntity);
        }

        public IEnumerable<Watched> GetAllUsersWatchedAMovie()
        {
            return _watchedMovieRepository.GetAllUsersWatchedAMovie();
        }

        public IEnumerable<Watched> GetAllWatchedMovies(string userId)
        {
            return _watchedMovieRepository.GetAllWatchedMovies(userId);
        }

        public Watched GetUserWatchedEntity(string userId)
        {
            return _watchedMovieRepository.GetUserWatchedEntity(userId);
        }
    }
}
