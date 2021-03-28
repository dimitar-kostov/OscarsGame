using OscarsGame.Domain.Entities;
using System.Threading.Tasks;

namespace OscarsGame.Domain.Repositories
{
    public interface ITMDBRepository
    {
        Task<Movie> GetMovieAsync(string movieID);
        Task<MoviesCollection> SearchMovieAsync(string searchString);

    }
}
