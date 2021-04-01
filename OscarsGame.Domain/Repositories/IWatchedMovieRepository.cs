using OscarsGame.Domain.Entities;
using System;
using System.Collections.Generic;

namespace OscarsGame.Domain.Repositories
{
    public interface IWatchedMovieRepository
    {
        Watched AddWatchedEntity(Watched watchedEntity);

        IEnumerable<Watched> GetAllUsersWatchedAMovie();

        IEnumerable<Watched> GetAllWatchedMovies(Guid userId);

        Watched GetUserWatchedEntity(Guid userId);
    }
}
