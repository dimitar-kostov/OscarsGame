using OscarsGame.Business.Interfaces;
using OscarsGame.Domain;
using OscarsGame.Domain.Entities;
using System;
using System.Collections.Generic;

namespace OscarsGame.Business
{
    public class WatchedMovieService : IWatchedMovieService
    {

        private readonly IUnitOfWork _unitOfWork;


        public WatchedMovieService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Watched AddWatchedEntity(Watched watchedEntity)
        {
            return _unitOfWork.WatchedMovieRepository.AddWatchedEntity(watchedEntity);
        }

        public IEnumerable<Watched> GetAllUsersWatchedAMovie()
        {
            return _unitOfWork.WatchedMovieRepository.GetAllUsersWatchedAMovie();
        }

        public IEnumerable<Watched> GetAllWatchedMovies(Guid userId)
        {
            return _unitOfWork.WatchedMovieRepository.GetAllWatchedMovies(userId);
        }

        public Watched GetUserWatchedEntity(Guid userId)
        {
            return _unitOfWork.WatchedMovieRepository.GetUserWatchedEntity(userId);
        }
    }
}
