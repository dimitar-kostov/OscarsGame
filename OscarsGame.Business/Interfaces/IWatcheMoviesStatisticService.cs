using System.Collections.Generic;

namespace OscarsGame.Business.Interfaces
{
    public interface IWatcheMoviesStatisticService
    {
        List<WatchedObject> GetData();
    }
}
