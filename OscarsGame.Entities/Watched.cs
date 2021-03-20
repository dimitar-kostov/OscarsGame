using OscarsGame.Entities.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OscarsGame.Entities
{
    public class Watched: IWatched
    {
        [Key]
        public string UserId { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

    }
}

