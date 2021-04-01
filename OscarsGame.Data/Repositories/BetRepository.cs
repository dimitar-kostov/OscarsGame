using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Models;
using OscarsGame.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OscarsGame.Data
{
    internal class BetRepository : Repository<Bet>, IBetRepository
    {
        internal BetRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Bet> GetAllUserBets(Guid userId)
        {
            return Context.Bets.Where(bet => bet.UserId == userId).ToList();
        }

        public IEnumerable<Bet> GetAllBetsByCategory(int categoryId)
        {
            return Context.Bets
                    .Include(b => b.Nomination)
                    .Include(b => b.Nomination.Movie)
                    .Include(b => b.User)
                    .Where(bet => bet.Nomination.Category.Id == categoryId).ToList();
        }

        public void MakeBetEntity(Guid userId, int nominationId)
        {
            var selectedNomination = Context.Nominations
                    .Include(x => x.Category)
                    .Where(x => x.Id == nominationId)
                    .SingleOrDefault();

            Bet categoryUserBet = Context.Bets
                .Include(x => x.Nomination)
                .Where(x => x.UserId == userId)
                .Where(x => x.Nomination.Category.Id == selectedNomination.Category.Id)
                .FirstOrDefault();

            if (categoryUserBet == null)
            {
                categoryUserBet = new Bet()
                {
                    UserId = userId,
                    Nomination = selectedNomination,
                };

                Context.Entry(categoryUserBet).State = EntityState.Added;
            }
            else if (categoryUserBet.Nomination.Id == selectedNomination.Id)
            {
                Context.Entry(categoryUserBet).State = EntityState.Deleted;
            }
            else
            {
                categoryUserBet.Nomination = selectedNomination;
                Context.Entry(categoryUserBet).State = EntityState.Modified;
            }

            Context.SaveChanges();
        }

        public IEnumerable<UserScore> GetAllUserScores()
        {
            var bets = Context.Bets
                    .GroupBy(x => x.User)
                    .Select(g => new
                    {
                        User = g.Key,
                        Score = g.Count(x => x.Nomination.IsWinner),
                        WatchedMovies = 0,
                        WatchedNominations = 0,
                        Bets = g.Count(),
                    });

            var watchedMovies = Context.Watched
                .GroupBy(x => x.User)
                .Select(g => new
                {
                    User = g.Key,
                    Score = 0,
                    WatchedMovies = g.Sum(x => x.Movies.Count),
                    WatchedNominations = g.Sum(x => x.Movies.SelectMany(m => m.Nominations).Count()),
                    Bets = 0,
                });

            return bets
                .Union(watchedMovies)
                .GroupBy(x => x.User)
                .Select(g => new UserScore
                {
                    UserDisplayName = g.Key.DisplayName,
                    Score = g.Sum(x => x.Score),
                    WatchedMovies = g.Sum(x => x.WatchedMovies),
                    WatchedNominations = g.Sum(x => x.WatchedNominations),
                    Bets = g.Sum(x => x.Bets),
                })
                .OrderByDescending(x => x.Score)
                .ThenByDescending(x => x.WatchedMovies)
                .ThenByDescending(x => x.WatchedNominations)
                .ThenByDescending(x => x.Bets)
                .ToList();
        }
    }
}
