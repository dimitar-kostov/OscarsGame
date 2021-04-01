using System;

namespace OscarsGame.Domain.Entities
{
    public class Bet
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public virtual Nomination Nomination { get; set; }
    }
}
