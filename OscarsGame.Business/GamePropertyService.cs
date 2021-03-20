using OscarsGame.Business.Interfaces;
using OscarsGame.Data;
using OscarsGame.Data.Interfaces;
using OscarsGame.Entities;
using System;

namespace OscarsGame.Business
{
    public class GamePropertyService: IGamePropertyService
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
            GameProperties dateObject = _gamePropertyRepository.GetDate();
            DateTime startDate = (dateObject != null ? dateObject.StartGameDate : DateTime.Now);
            return (startDate > DateTime.Now);
        }

        public bool IsGameStopped()
        {
            GameProperties stopDateObject = _gamePropertyRepository.GetDate();
            DateTime stopDate = (stopDateObject != null ? stopDateObject.StopGameDate : DateTime.Now);
            return (stopDate < DateTime.Now);
        }
    }
}
