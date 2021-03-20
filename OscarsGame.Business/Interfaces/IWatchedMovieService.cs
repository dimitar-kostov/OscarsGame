using OscarsGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
