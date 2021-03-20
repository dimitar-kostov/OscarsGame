using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OscarsGame.Entities.Interfaces
{
    public interface IMovie
    {
        int Id { get; set; }
        string Title { get; set; }    
        string ReleaseDate { get; set; }       
        string PosterPath { get; set; }
        string Overview { get; set; }
        string ImdbId { get; set; }

        ICollection<Category> Categories { get; set; }
        ICollection<Watched> UsersWatchedThisMovie { get; set; }
        ICollection<Bet> Bets { get; set; }
        ICollection<Category> WinningCategories { get; set; }
    }
}
