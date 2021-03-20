using OscarsGame.Entities;
using System.Threading.Tasks;

namespace OscarsGame.Data.Interfaces
{
    public interface ITMDBRepository
    {
        Task<Movie> GetMovieAsync(string movieID);
        Task<MoviesCollection> SearchMovieAsync(string searchString);

    }
}
