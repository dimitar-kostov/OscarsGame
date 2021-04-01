using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Repositories;
using System;
using System.Linq;


namespace OscarsGame.Data
{
    internal class GamePropertyRepository : Repository<GameProperties>, IGamePropertyRepository
    {
        internal GamePropertyRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public void ChangeGameStartDate(DateTime startDate)
        {
            var foundedEntity = Context.Game.FirstOrDefault();
            if (foundedEntity == null)
            {
                var newDateEntity = new GameProperties { StopGameDate = startDate, StartGameDate = startDate }; //To Do!
                Context.Game.Add(newDateEntity);
            }
            else
            {
                foundedEntity.StartGameDate = startDate;
            }

            Context.SaveChanges();
        }

        public void ChangeGameStopDate(DateTime stopDate)
        {
            var foundedEntity = Context.Game.FirstOrDefault();
            if (foundedEntity == null)
            {
                var stopDateEntity = new GameProperties { StopGameDate = stopDate, StartGameDate = stopDate }; //To Do!
                Context.Game.Add(stopDateEntity);
            }
            else
            {
                foundedEntity.StopGameDate = stopDate;
            }

            Context.SaveChanges();
        }

        public DateTime GetGameStartDate()
        {
            return Context.Game.Select(x => x.StartGameDate).SingleOrDefault();
        }

        public DateTime GetGameStopDate()
        {
            return Context.Game.Select(x => x.StopGameDate).SingleOrDefault();
        }

        public GameProperties GetDate()
        {
            return Context.Game.FirstOrDefault();
        }
    }
}
