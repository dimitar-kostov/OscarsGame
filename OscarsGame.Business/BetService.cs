using OscarsGame.Business.Interfaces;
using OscarsGame.Data.Interfaces;
using OscarsGame.Entities;
using OscarsGame.Entities.StatisticsModels;
using System;
using System.Collections.Generic;

namespace OscarsGame.Business
{
    public class BetService: IBetService
    {
        private readonly IBetRepository _betRepository;

        public BetService(IBetRepository betRepository)
        {
            _betRepository = betRepository;
        }

        public IEnumerable<Bet> GetAllUserBets(string userId)
        {
            return _betRepository.GetAllUserBets(userId);
        }

        public IEnumerable<Bet> GetAllBetsByCategory(int categoryId)
        {
            return _betRepository.GetAllBetsByCategory(categoryId);
        }

        public void MakeBetEntity(string userId, int nominationId)
        {
            _betRepository.MakeBetEntity(userId, nominationId);
        }

        public IEnumerable<UserScore> GetAllUserScores()
        {
            var allScores = _betRepository.GetAllUserScores();

            int rank = 1;
            foreach (UserScore userScore in allScores)
            {
                userScore.Rank = rank++;
            }

            return allScores;
        }
    }

}
