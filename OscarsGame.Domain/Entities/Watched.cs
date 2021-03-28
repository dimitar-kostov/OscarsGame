using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OscarsGame.Domain.Entities
{
    public class Watched
    {
        [Key]
        public string UserId { get; set; }

        //public int Id { get; set; }

        //public Guid UserId { get; set; }
        //public virtual User User { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}

