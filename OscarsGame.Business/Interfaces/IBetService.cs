using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Models;
using System;
using System.Collections.Generic;

namespace OscarsGame.Business.Interfaces
{
    public interface IBetService
    {
        IEnumerable<Bet> GetAllUserBets(Guid userId);

        IEnumerable<Bet> GetAllBetsByCategory(int categoryId);

        void MakeBetEntity(Guid userId, int nominationId);

        IEnumerable<UserScore> GetAllUserScores();
    }
}
