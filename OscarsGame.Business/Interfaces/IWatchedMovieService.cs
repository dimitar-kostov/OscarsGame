using OscarsGame.Domain.Entities;
using System;
using System.Collections.Generic;

namespace OscarsGame.Business.Interfaces
{
    public interface IWatchedMovieService
    {
        Watched AddWatchedEntity(Watched watchedEntity);

        IEnumerable<Watched> GetAllUsersWatchedAMovie();

        IEnumerable<Watched> GetAllWatchedMovies(Guid userId);

        Watched GetUserWatchedEntity(Guid userId);

    }
}
