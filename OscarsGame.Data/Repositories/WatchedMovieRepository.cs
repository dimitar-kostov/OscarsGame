using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OscarsGame.Data
{
    internal class WatchedMovieRepository : Repository<Watched>, IWatchedMovieRepository
    {
        internal WatchedMovieRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public Watched AddWatchedEntity(Watched watchedEntity)
        {
            watchedEntity = Context.Watched.Add(watchedEntity);
            Context.SaveChanges();

            return watchedEntity;
        }

        public IEnumerable<Watched> GetAllUsersWatchedAMovie()
        {
            return Context.Watched.Include(w => w.Movies).ToList();
        }

        public IEnumerable<Watched> GetAllWatchedMovies(Guid userId)
        {
            return Context.Watched.Include(w => w.Movies).Where(x => x.UserId == userId).ToList();
        }

        public Watched GetUserWatchedEntity(Guid userId)
        {
            return Context.Watched.Where(x => x.UserId == userId).SingleOrDefault();
        }
    }
}
