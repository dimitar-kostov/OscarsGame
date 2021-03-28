using OscarsGame.Business.Interfaces;
using OscarsGame.Data.Interfaces;
using OscarsGame.Entities;
using System;

namespace OscarsGame.Business
{
    public class GamePropertyService : IGamePropertyService
    {
        private readonly IGamePropertyRepository _gamePropertyRepository;


        public GamePropertyService(IGamePropertyRepository gamePropertyRepository)
        {
            _gamePropertyRepository = gamePropertyRepository;
        }

        public void ChangeGameStartDate(DateTime stopDate)
        {
            _gamePropertyRepository.ChangeGameStartDate(stopDate);
        }

        public void ChangeGameStopDate(DateTime stopDate)
        {
            _gamePropertyRepository.ChangeGameStopDate(stopDate);
        }

        public DateTime GetGameStartDate()
        {
            return _gamePropertyRepository.GetGameStartDate();
        }

        public DateTime GetGameStopDate()
        {
            return _gamePropertyRepository.GetGameStopDate();
        }

        public bool IsGameNotStartedYet()
        {
            DateTime now = DateTime.Now;
            GameProperties dateObject = _gamePropertyRepository.GetDate();
            DateTime startDate = (dateObject != null ? dateObject.StartGameDate : now);
            return (startDate > now);
        }

        public bool IsGameStopped()
        {
            DateTime now = DateTime.Now;
            GameProperties stopDateObject = _gamePropertyRepository.GetDate();
            DateTime stopDate = (stopDateObject != null ? stopDateObject.StopGameDate : now);
            return (stopDate < now);
        }
    }
}
