using OscarsGame.Business.Interfaces;
using Unity;

namespace OscarsGame.Business
{
    public static class BusinessContainerManager
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            // Register services 
            container.RegisterType<IBetService, BetService>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IGamePropertyService, GamePropertyService>();
            container.RegisterType<IMovieService, MovieService>();
            container.RegisterType<IWatchedMovieService, WatchedMovieService>();
            container.RegisterType<IWatcheMoviesStatisticService, WatcheMoviesStatisticService>();
            container.RegisterType<INominationService, NominationService>();
        }
    }
}
