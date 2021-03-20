using System.Collections.Generic;

namespace OscarsGame.Entities.Interfaces
{
    interface ICategory
    {
        int Id { get; set; }
        string CategoryTtle { get; set; }
        string CategoryDescription { get; set; }
        ICollection<Movie> Movies { get; set; }
        ICollection<Bet> Bets { get; set; }
        Movie Winner { get; set; }
    }
}
