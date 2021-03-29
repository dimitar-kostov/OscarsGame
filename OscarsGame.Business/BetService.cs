using OscarsGame.Business.Interfaces;
using OscarsGame.Domain;
using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Models;
using System.Collections.Generic;

namespace OscarsGame.Business
{
    public class BetService : IBetService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Bet> GetAllUserBets(string userId)
        {
            return _unitOfWork.BetRepository.GetAllUserBets(userId);
        }

        public IEnumerable<Bet> GetAllBetsByCategory(int categoryId)
        {
            return _unitOfWork.BetRepository.GetAllBetsByCategory(categoryId);
        }

        public void MakeBetEntity(string userId, int nominationId)
        {
            _unitOfWork.BetRepository.MakeBetEntity(userId, nominationId);
        }

        public IEnumerable<UserScore> GetAllUserScores()
        {
            var allScores = _unitOfWork.BetRepository.GetAllUserScores();

            int rank = 1;
            foreach (UserScore userScore in allScores)
            {
                userScore.Rank = rank++;
            }

            return allScores;
        }
    }

}
