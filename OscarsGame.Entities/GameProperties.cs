using OscarsGame.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OscarsGame.Entities
{
    public class GameProperties: IGameProperties
    {
        public int Id { get; set; }
        public DateTime StopGameDate { get; set; }
        public DateTime StartGameDate { get; set; }
    }
}
