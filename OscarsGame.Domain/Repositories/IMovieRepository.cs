using OscarsGame.Domain.Entities;
using System;
using System.Collections.Generic;

namespace OscarsGame.Domain.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        void AddMovie(Movie movie);

        void OverrideMovie(Movie movie);

        void ChangeMovieStatus(Guid userId, int movieId);

        IEnumerable<Movie> GetAllMovies();

        Movie GetMovie(int id);

        bool HasMovie(int id);

    }
}
