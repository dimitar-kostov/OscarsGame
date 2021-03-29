using OscarsGame.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OscarsGame.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IBetRepository BetRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IGamePropertyRepository GamePropertyRepository { get; }
        IMovieRepository MovieRepository { get; }
        INominationRepository NominationRepository { get; }
        IViewModelsRepository ViewModelsRepository { get; }
        IWatchedMovieRepository WatchedMovieRepository { get; }

        #region Properties
        //IExternalLoginRepository ExternalLoginRepository { get; }
        //IRoleRepository RoleRepository { get; }
        //IUserRepository UserRepository { get; }
        #endregion

        #region Methods
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        #endregion
    }
}
