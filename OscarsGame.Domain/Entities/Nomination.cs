using System.Collections.Generic;

namespace OscarsGame.Domain.Entities
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
