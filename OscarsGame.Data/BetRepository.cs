using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Models;
using OscarsGame.Domain.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OscarsGame.Data
{
    public class BetRepository : IBetRepository
    {
        public IEnumerable<Bet> GetAllUserBets(string userId)
        {
            using (var ctx = new MovieContext())
            {
                var bets = ctx.Bets.Where(bet => bet.UserId == userId).ToList();
                return bets;
            }
        }

        public IEnumerable<Bet> GetAllBetsByCategory(int categoryId)
        {
            using (var ctx = new MovieContext())
            {
                return ctx.Bets
                    .Include(b => b.Nomination).Include(b => b.Nomination.Movie)
                    .Where(bet => bet.Nomination.Category.Id == categoryId).ToList();
            }
        }

        public void MakeBetEntity(string userId, int nominationId)
        {
            using (var ctx = new MovieContext())
            {
                var selectedNomination = ctx.Nominations
                    .Include(x => x.Category)
                    .Where(x => x.Id == nominationId)
                    .SingleOrDefault();

                Bet categoryUserBet = ctx.Bets
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

                    ctx.Entry(categoryUserBet).State = EntityState.Added;
                }
                else if (categoryUserBet.Nomination.Id == selectedNomination.Id)
                {
                    ctx.Entry(categoryUserBet).State = EntityState.Deleted;
                }
                else
                {
                    categoryUserBet.Nomination = selectedNomination;
                    ctx.Entry(categoryUserBet).State = EntityState.Modified;
                }

                ctx.SaveChanges();
            }
        }

        public IEnumerable<UserScore> GetAllUserScores()
        {
            using (var ctx = new MovieContext())
            {
                var bets = ctx.Bets
                    .GroupBy(x => x.UserId)
                    .Select(g => new
                    {
                        UserId = g.Key,
                        Score = g.Count(x => x.Nomination.IsWinner),
                        WatchedMovies = 0,
                        WatchedNominations = 0,
                        Bets = g.Count(),
                    });

                var watchedMovies = ctx.Watched
                    .GroupBy(x => x.UserId)
                    .Select(g => new
                    {
                        UserId = g.Key,
                        Score = 0,
                        WatchedMovies = g.Sum(x => x.Movies.Count),
                        WatchedNominations = g.Sum(x => x.Movies.SelectMany(m => m.Nominations).Count()),
                        Bets = 0,
                    });

                return bets
                    .Union(watchedMovies)
                    .GroupBy(x => x.UserId)
                    .Select(g => new UserScore
                    {
                        Email = g.Key,
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
}
