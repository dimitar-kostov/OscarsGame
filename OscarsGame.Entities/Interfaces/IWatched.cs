using System.Collections.Generic;

namespace OscarsGame.Entities.Interfaces
{
    interface IWatched
    {
        string UserId { get; set; }
        ICollection<Movie> Movies { get; set; }
    }
}
