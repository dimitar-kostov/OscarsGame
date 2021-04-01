using OscarsGame.Domain.Entities;
using System;

namespace OscarsGame.Domain.Repositories
{
    public interface IGamePropertyRepository : IRepository<GameProperties>
    {
        void ChangeGameStartDate(DateTime startDate);

        void ChangeGameStopDate(DateTime stopDate);

        DateTime GetGameStartDate();

        DateTime GetGameStopDate();

        GameProperties GetDate();
    }
}
