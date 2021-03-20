using OscarsGame.Entities;
using OscarsGame.Entities.StatisticsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OscarsGame.Business.Interfaces
{
    public interface IBetService
    {
        IEnumerable<Bet> GetAllUserBets(string userId);

        IEnumerable<Bet> GetAllBetsByCategory(int categoryId);

        void MakeBetEntity(string userId, int nominationId);

        IEnumerable<UserScore> GetAllUserScores();
    }
}
