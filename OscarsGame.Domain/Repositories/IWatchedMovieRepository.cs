using OscarsGame.Domain.Entities;
using System.Collections.Generic;

namespace OscarsGame.Domain.Repositories
{
    public interface IWatchedMovieRepository
    {
        Watched AddWatchedEntity(Watched watchedEntity);

        IEnumerable<Watched> GetAllUsersWatchedAMovie();

        IEnumerable<Watched> GetAllWatchedMovies(string userId);

        Watched GetUserWatchedEntity(string userId);
    }
}
