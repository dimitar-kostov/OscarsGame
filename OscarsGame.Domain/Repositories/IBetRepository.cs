using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Models;
using System.Collections.Generic;

namespace OscarsGame.Domain.Repositories
{
    public interface IBetRepository : IRepository<Bet>
    {
        IEnumerable<Bet> GetAllUserBets(string userId);

        IEnumerable<Bet> GetAllBetsByCategory(int categoryId);

        void MakeBetEntity(string userId, int nominationId);

        IEnumerable<UserScore> GetAllUserScores();
    }
}
