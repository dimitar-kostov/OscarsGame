using OscarsGame.Business.Interfaces;
using OscarsGame.Domain;
using OscarsGame.Domain.Entities;
using System;

namespace OscarsGame.Business
{
    public class GamePropertyService : IGamePropertyService
    {
        private readonly IUnitOfWork _unitOfWork;


        public GamePropertyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void ChangeGameStartDate(DateTime stopDate)
        {
            _unitOfWork.GamePropertyRepository.ChangeGameStartDate(stopDate);
        }

        public void ChangeGameStopDate(DateTime stopDate)
        {
            _unitOfWork.GamePropertyRepository.ChangeGameStopDate(stopDate);
        }

        public DateTime GetGameStartDate()
        {
            return _unitOfWork.GamePropertyRepository.GetGameStartDate();
        }

        public DateTime GetGameStopDate()
        {
            return _unitOfWork.GamePropertyRepository.GetGameStopDate();
        }

        public bool IsGameNotStartedYet()
        {
            DateTime now = DateTime.Now;
            GameProperties dateObject = _unitOfWork.GamePropertyRepository.GetDate();
            DateTime startDate = (dateObject != null ? dateObject.StartGameDate : now);
            return (startDate > now);
        }

        public bool IsGameStopped()
        {
            DateTime now = DateTime.Now;
            GameProperties stopDateObject = _unitOfWork.GamePropertyRepository.GetDate();
            DateTime stopDate = (stopDateObject != null ? stopDateObject.StopGameDate : now);
            return (stopDate < now);
        }
    }
}
