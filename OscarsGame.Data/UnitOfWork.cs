using OscarsGame.Domain;
using OscarsGame.Domain.Repositories;
using System.Threading.Tasks;

namespace OscarsGame.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private readonly ApplicationDbContext _context;

        private IBetRepository _betRepository;
        private ICategoryRepository _categoryRepository;
        private IGamePropertyRepository _gamePropertyRepository;
        private IMovieRepository _movieRepository;
        private INominationRepository _nominationRepository;
        private IViewModelsRepository _viewModelsRepository;
        private IWatchedMovieRepository _watchedMovieRepository;


        //private IExternalLoginRepository _externalLoginRepository;
        //private IRoleRepository _roleRepository;
        //private IUserRepository _userRepository;
        #endregion

        #region Constructors
        public UnitOfWork(string nameOrConnectionString)
        {
            _context = new ApplicationDbContext(nameOrConnectionString);
        }
        #endregion

        #region IUnitOfWork Members

        public IBetRepository BetRepository =>
            _betRepository ?? (_betRepository = new BetRepository(_context));

        public ICategoryRepository CategoryRepository =>
            _categoryRepository ?? (_categoryRepository = new CategoryRepository(_context));

        public IGamePropertyRepository GamePropertyRepository =>
            _gamePropertyRepository ?? (_gamePropertyRepository = new GamePropertyRepository(_context));

        public IMovieRepository MovieRepository =>
            _movieRepository ?? (_movieRepository = new MovieRepository(_context));

        public INominationRepository NominationRepository =>
            _nominationRepository ?? (_nominationRepository = new NominationRepository(_context));

        public IViewModelsRepository ViewModelsRepository =>
            _viewModelsRepository ?? (_viewModelsRepository = new ViewModelsRepository(_context));

        public IWatchedMovieRepository WatchedMovieRepository =>
            _watchedMovieRepository ?? (_watchedMovieRepository = new WatchedMovieRepository(_context));

        //public IExternalLoginRepository ExternalLoginRepository
        //{
        //    get { return _externalLoginRepository ?? (_externalLoginRepository = new ExternalLoginRepository(_context)); }
        //}

        //public IRoleRepository RoleRepository
        //{
        //    get { return _roleRepository ?? (_roleRepository = new RoleRepository(_context)); }
        //}

        //public IUserRepository UserRepository
        //{
        //    get { return _userRepository ?? (_userRepository = new UserRepository(_context)); }
        //}

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            //_externalLoginRepository = null;
            //_roleRepository = null;
            //_userRepository = null;
            _context.Dispose();
        }
        #endregion
    }
}
