using Microsoft.Practices.Unity;
using OscarsGame.Business.Interfaces;
using OscarsGame.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OscarsGame.Business
{
    public class BusinessContainerManager
    {
        public void RegisterTypes(IUnityContainer container)
        {
            // Register services 
            container.RegisterType<IBetService, BetService>();
            container.RegisterType<IBetStatisticService, BetsStatisticService>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IGamePropertyService, GamePropertyService>();
            container.RegisterType<IMovieService, MovieService>();
            container.RegisterType<IWatchedMovieService, WatchedMovieService>();
            container.RegisterType<IWatcheMoviesStatisticService, WatcheMoviesStatisticService>();
            container.RegisterType<INominationService, NominationService>();

            // Register data types
            DataContainerManager containerManager = new DataContainerManager();
            containerManager.RegisterTypes(container);
        }
    }
}
