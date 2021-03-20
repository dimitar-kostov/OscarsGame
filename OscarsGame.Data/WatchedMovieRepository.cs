using OscarsGame.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using OscarsGame.Data.Interfaces;

namespace OscarsGame.Data
{
    public class WatchedMovieRepository: IWatchedMovieRepository
    {
        public Watched AddWatchedEntity(Watched watchedEntity)
        {
            using (var ctx = new MovieContext())
            {
                watchedEntity = ctx.Watched.Add(watchedEntity);
                ctx.SaveChanges();
            }

            return watchedEntity;
        }

        public IEnumerable<Watched> GetAllUsersWatchedAMovie()
        {
            using (var ctx = new MovieContext())
            {
                return ctx.Watched.Include(w => w.Movies).ToList();
            }
        }

        public IEnumerable<Watched> GetAllWatchedMovies(string userId)
        {
            using (var ctx = new MovieContext())
            {
                var watched = ctx.Watched.Include(w => w.Movies).Where(x => x.UserId == userId).ToList();

                return watched;
            }
        }

        public Watched GetUserWatchedEntity(string userId)
        {
            using (var ctx = new MovieContext())
            {
                var foundedEntity = ctx.Watched.Where(x => x.UserId == userId).SingleOrDefault();
                return foundedEntity;
            }
        }
       
    }
}
