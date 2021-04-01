using System;

namespace OscarsGame.Domain.Entities
{
    public class GameProperties
    {
        public int Id { get; set; }
        public DateTime StopGameDate { get; set; }
        public DateTime StartGameDate { get; set; }
    }
}
