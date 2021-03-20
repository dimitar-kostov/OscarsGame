using System;

namespace OscarsGame.Business.Interfaces
{
    public interface IGamePropertyService
    {
        void ChangeGameStartDate(DateTime stopDate);

        void ChangeGameStopDate(DateTime stopDate);

        DateTime GetGameStartDate();

        DateTime GetGameStopDate();

        bool IsGameNotStartedYet();

        bool IsGameStopped();
        
    }
}
