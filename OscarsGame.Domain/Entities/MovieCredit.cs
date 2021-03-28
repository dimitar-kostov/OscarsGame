using System;
using System.Collections.Generic;

namespace OscarsGame.Domain.Entities
{
    [Serializable]
    public class MovieCredit
    {
        public string Id { get; set; }

        public int Order { get; set; }

        public int PersonId { get; set; }

        public string Name { get; set; }

        public bool IsCast { get; set; }

        /// <summary>
        /// Cast chracter or Crew job
        /// </summary>
        public string Role { get; set; }

        public string ProfilePath { get; set; }

        public Movie Movie { get; set; }

        public virtual ICollection<Nomination> Nominations { get; set; }
    }
}
