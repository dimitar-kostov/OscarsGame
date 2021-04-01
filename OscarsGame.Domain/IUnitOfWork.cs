using OscarsGame.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OscarsGame.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        #region Properties
        IBetRepository BetRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IGamePropertyRepository GamePropertyRepository { get; }
        IMovieRepository MovieRepository { get; }
        INominationRepository NominationRepository { get; }
        IViewModelsRepository ViewModelsRepository { get; }
        IWatchedMovieRepository WatchedMovieRepository { get; }

        IExternalLoginRepository ExternalLoginRepository { get; }
        IUserRepository UserRepository { get; }
        #endregion

        #region Methods
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        #endregion
    }
}
