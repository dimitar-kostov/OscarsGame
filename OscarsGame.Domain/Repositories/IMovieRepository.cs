using OscarsGame.Domain.Entities;
using System.Collections.Generic;

namespace OscarsGame.Domain.Repositories
{
    public interface IMovieRepository
    {
        void AddMovie(Movie movie);

        void OverrideMovie(Movie movie);

        void ChangeMovieStatus(string userId, int movieId);

        IEnumerable<Movie> GetAllMovies();

        Movie GetMovie(int id);

        bool HasMovie(int id);

    }
}
