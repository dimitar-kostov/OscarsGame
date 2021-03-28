using OscarsGame.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OscarsGame.Business.Interfaces
{
    public interface IMovieClient
    {
        Task<Movie> GetMovieAsync(string movieID);

        Task<List<Movie>> SearchMovieAsync(string searchString);
    }
}
