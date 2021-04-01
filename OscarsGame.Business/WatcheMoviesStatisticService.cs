using OscarsGame.Business.Interfaces;
using OscarsGame.Domain;
using OscarsGame.Domain.Models;
using System;
using System.Collections.Generic;
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

            var watchedDict = new Dictionary<Guid, WatchedObject>();

            foreach (var watchedEntity in watchedEntities)
            {
                if (watchedEntity.UserId.HasValue)
                {
                    if (!watchedDict.TryGetValue(watchedEntity.UserId.Value, out WatchedObject watchedObject))
                    {
                        watchedObject = new WatchedObject
                        {
                            UserDisplayMail = watchedEntity.UserDisplayName,
                            MovieTitles = new List<string>()
                        };

                        watchedDict.Add(watchedEntity.UserId.Value, watchedObject);
                    }

                    watchedObject.MovieTitles.Add(watchedEntity.MovieTitle);
                }
            }

            return watchedDict.Values.ToList();
        }

    }
}

