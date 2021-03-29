using OscarsGame.Business.Interfaces;
using OscarsGame.Domain;
using OscarsGame.Domain.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace OscarsGame.Business
{
    public class WatcheMoviesStatisticService : IWatcheMoviesStatisticService
    {
        private readonly IUnitOfWork _unitOfWork;


        public WatcheMoviesStatisticService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<WatchedObject> GetData()
        {
            List<WatchedMovies> watchedEntities = _unitOfWork.ViewModelsRepository.GetWatchedMoviesData();

            Dictionary<string, List<string>> watchedDict = new Dictionary<string, List<string>>();

            foreach (var watchedEntity in watchedEntities)
            {
                if (watchedEntity.Email != null)
                {
                    List<string> watchedMoviesList;

                    if (!watchedDict.TryGetValue(watchedEntity.Email, out watchedMoviesList))
                    {
                        watchedMoviesList = new List<string>();
                        watchedDict.Add(watchedEntity.Email, watchedMoviesList);
                    }

                    watchedMoviesList.Add(watchedEntity.Title);
                }
            }

            return watchedDict.Select(x => new WatchedObject { UserEmail = x.Key, MovieTitles = x.Value }).ToList();
        }

        public string[] GetTitles()
        {
            var watchedMovies = _unitOfWork.ViewModelsRepository.GetWatchedMoviesData();
            var stringArrTitles = watchedMovies.Select(m => m.Title).ToArray();
            var collectionWithDistinctTitles = stringArrTitles.Distinct().ToArray();
            int titlesCount = collectionWithDistinctTitles.Count();
            return collectionWithDistinctTitles;
        }

    }
}

