using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OscarsGame.Entities
{
    public class Nomination
    {   
        public int Id { get; set; }

        public Category Category { get; set; }

        public Movie Movie { get; set; }

        public virtual ICollection<MovieCredit> Credits { get; set; }

        public bool IsWinner { get; set; }

        public virtual ICollection<Bet> Bets { get; set; }
    }
}
