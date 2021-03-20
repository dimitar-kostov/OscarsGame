using OscarsGame.Entities;
using OscarsGame.Entities.StatisticsModels;
using System.Collections.Generic;

namespace OscarsGame.Data.Interfaces
{
    public interface IBetRepository
    {
        IEnumerable<Bet> GetAllUserBets(string userId);

        IEnumerable<Bet> GetAllBetsByCategory(int categoryId);

        void MakeBetEntity(string userId, int nominationId);

        IEnumerable<UserScore> GetAllUserScores();
    }
}
