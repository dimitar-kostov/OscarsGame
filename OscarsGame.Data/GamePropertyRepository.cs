using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Repositories;
using System;
using System.Linq;


namespace OscarsGame.Data
{
    public class GamePropertyRepository : IGamePropertyRepository
    {
        public void ChangeGameStartDate(DateTime startDate)
        {
            using (var ctx = new MovieContext())
            {
                var foundedEntity = ctx.Game.FirstOrDefault();
                if (foundedEntity == null)
                {
                    var newDateEntity = new GameProperties { StopGameDate = startDate, StartGameDate = startDate }; //To Do!
                    ctx.Game.Add(newDateEntity);
                }
                else
                {
                    foundedEntity.StartGameDate = startDate;
                }

                ctx.SaveChanges();
            }
        }

        public void ChangeGameStopDate(DateTime stopDate)
        {
            using (var ctx = new MovieContext())
            {
                var foundedEntity = ctx.Game.FirstOrDefault();
                if (foundedEntity == null)
                {
                    var stopDateEntity = new GameProperties { StopGameDate = stopDate, StartGameDate = stopDate }; //To Do!
                    ctx.Game.Add(stopDateEntity);
                }
                else
                {
                    foundedEntity.StopGameDate = stopDate;
                }

                ctx.SaveChanges();
            }
        }

        public DateTime GetGameStartDate()
        {
            using (var ctx = new MovieContext())
            {
                var foundedDate = ctx.Game.Select(x => x.StartGameDate).SingleOrDefault();
                return foundedDate;
            }
        }

        public DateTime GetGameStopDate()
        {
            using (var ctx = new MovieContext())
            {
                var foundedDate = ctx.Game.Select(x => x.StopGameDate).SingleOrDefault();
                return foundedDate;
            }
        }

        public GameProperties GetDate()
        {
            using (var ctx = new MovieContext())
            {
                var foundedDate = ctx.Game.FirstOrDefault();
                return foundedDate;
            }
        }
    }
}
