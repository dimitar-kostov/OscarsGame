using System;
using System.Collections.Generic;

namespace OscarsGame.Domain.Entities
{
    public class Watched
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}

