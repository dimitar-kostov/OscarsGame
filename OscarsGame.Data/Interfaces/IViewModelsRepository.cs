using OscarsGame.Entities.StatisticsModels;
using System.Collections.Generic;

namespace OscarsGame.Data.Interfaces
{
    public interface IViewModelsRepository
    {
        List<WatchedMovies> GetWatchedMoviesData();

        List<BetsStatistic> GetBetsData();

        List<Winners> GetWinner();
        
    }
}
