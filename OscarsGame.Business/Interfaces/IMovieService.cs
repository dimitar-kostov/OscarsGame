using OscarsGame.Business.Enums;
using OscarsGame.Domain.Entities;
using System;
using System.Collections.Generic;

namespace OscarsGame.Business.Interfaces
{
    public interface IMovieService
    {
        void ChangeMovieStatus(Guid userId, int movieId);

        IEnumerable<Movie> GetAllMovies();

        IEnumerable<Movie> GetAllMoviesByCriteria(Guid userId, OrderType orderType, FilterType filterType);

        Movie GetMovie(int id);

        bool HasMovie(int id);
    }
}
