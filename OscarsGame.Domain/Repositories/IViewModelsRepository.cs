using OscarsGame.Domain.Models;
using System.Collections.Generic;

namespace OscarsGame.Domain.Repositories
{
    public interface IViewModelsRepository
    {
        List<WatchedMovies> GetWatchedMoviesData();

        List<BetsStatistic> GetBetsData();

        List<Winners> GetWinner();

    }
}
