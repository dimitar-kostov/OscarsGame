using OscarsGame.Domain.Entities;
using System.Collections.Generic;

namespace OscarsGame.Business.Interfaces
{
    public interface IWatchedMovieService
    {
        Watched AddWatchedEntity(Watched watchedEntity);

        IEnumerable<Watched> GetAllUsersWatchedAMovie();

        IEnumerable<Watched> GetAllWatchedMovies(string userId);

        Watched GetUserWatchedEntity(string userId);

    }
}
